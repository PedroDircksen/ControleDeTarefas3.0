using ControleDeTarefas.Domínio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleDeTarefas.Controladores
{
    public class ControladorCompromisso : Controlador<Compromisso>
    {
        public override void EditarRegistro(int idSelecionado, Compromisso registro)
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoAtualizacao = new SqlCommand();
            comandoAtualizacao.Connection = conexaoComBanco;

            string sqlAtualizacao =
                @"UPDATE TBCOMPROMISSOS
	                SET	
		                [ASSUNTO] = @ASSUNTO,           
		                [LOCAL] = @LOCAL, 
		                [DATACOMPROMISSO] = @DATACOMPROMISSO, 
		                [HORAINICIO] = @HORAINICIO, 
		                [HORATERMINO] = @HORATERMINO, 
		                [ID_CONTATO] = @ID_CONTATO   
	                WHERE 
		                [ID] = @ID";

            comandoAtualizacao.CommandText = sqlAtualizacao;

            comandoAtualizacao.Parameters.AddWithValue("ID", idSelecionado);
            comandoAtualizacao.Parameters.AddWithValue("ASSUNTO", registro.assunto);
            comandoAtualizacao.Parameters.AddWithValue("LOCAL", registro.local);
            comandoAtualizacao.Parameters.AddWithValue("DATACOMPROMISSO", registro.dataCompromisso);
            comandoAtualizacao.Parameters.AddWithValue("HORAINICIO", registro.horaInicio);
            comandoAtualizacao.Parameters.AddWithValue("HORATERMINO", registro.horaTermino);
            comandoAtualizacao.Parameters.AddWithValue("ID_CONTATO", registro.idContato);

            comandoAtualizacao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }

        public override void ExcluirRegistro(Compromisso registro)
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao =
                @"DELETE FROM TBCOMPROMISSOS	                
	                WHERE 
		                [ID] = @ID";

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", registro.id);

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }

        public override void InserirNovo(Compromisso registro)
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

            string sqlInsercao =
                    @"INSERT INTO TBCOMPROMISSOS 
	                (		
		                [ASSUNTO], 
		                [LOCAL], 
		                [DATACOMPROMISSO], 
		                [HORAINICIO], 
		                [HORATERMINO], 
		                [ID_CONTATO]
	                ) 
	                VALUES 
	                (		
		                @ASSUNTO, 
		                @LOCAL,
		                @DATACOMPROMISSO,
		                @HORAINICIO,
		                @HORATERMINO,
		                @ID_CONTATO
	                )";

            sqlInsercao +=
                @"SELECT SCOPE_IDENTITY();";

            comandoInsercao.CommandText = sqlInsercao;

            comandoInsercao.Parameters.AddWithValue("ASSUNTO", registro.assunto);
            comandoInsercao.Parameters.AddWithValue("LOCAL", registro.local);
            comandoInsercao.Parameters.AddWithValue("DATACOMPROMISSO", registro.dataCompromisso);
            comandoInsercao.Parameters.AddWithValue("HORAINICIO", registro.horaInicio);
            comandoInsercao.Parameters.AddWithValue("HORATERMINO", registro.horaTermino);            

            if(registro.idContato == 0)
                comandoInsercao.Parameters.AddWithValue("ID_CONTATO", DBNull.Value);
            else
                comandoInsercao.Parameters.AddWithValue("ID_CONTATO", registro.idContato);


            object id = comandoInsercao.ExecuteScalar();

            registro.id = Convert.ToInt32(id);

            conexaoComBanco.Close();
        }

        public override Compromisso SelecionarRegistroPorId(int id)
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID],
                        [ASSUNTO], 
		                [LOCAL], 
		                [DATACOMPROMISSO], 
		                [HORAINICIO], 
		                [HORATERMINO], 
		                [ID_CONTATO]   
                    FROM 
                        TBCOMPROMISSOS
                    WHERE 
                        ID = @ID";

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", id);

            SqlDataReader leitorCompromisso = comandoSelecao.ExecuteReader();

            if (leitorCompromisso.Read() == null)
                return null;

            int idCompromisso = Convert.ToInt32(leitorCompromisso["ID"]);
            string assunto = Convert.ToString(leitorCompromisso["ASSUNTO"]);
            string local = Convert.ToString(leitorCompromisso["LOCAL"]);
            DateTime dataComromisso = Convert.ToDateTime(leitorCompromisso["DATACOMPROMISSO"]);
            string strHoraInicio = Convert.ToString(leitorCompromisso["HORAINICIO"]);
            string strHoraTermino = Convert.ToString(leitorCompromisso["HORATERMINO"]);
            int idContato = Convert.ToInt32(leitorCompromisso["ID_CONTATO"]);

            TimeSpan horaInicio;
            TimeSpan.TryParse(strHoraInicio, out horaInicio);
            TimeSpan horaTermino;
            TimeSpan.TryParse(strHoraTermino, out horaTermino);

            Compromisso compromisso = new Compromisso(idContato, assunto, local, dataComromisso, horaInicio, horaTermino);
            compromisso.id = idCompromisso;

            conexaoComBanco.Close();
            return compromisso;
        }

        public List<Compromisso> SelecionarTodosCompromissos()
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                    F.ID,
                    F.ASSUNTO, 
		            F.LOCAL, 
		            F.DATACOMPROMISSO, 
		            F.HORAINICIO, 
		            F.HORATERMINO, 
		            C.NOME
                FROM 
                    TBCOMPROMISSOS F LEFT JOIN
					TBCONTATOS C
				ON
					F.ID_CONTATO = C.ID";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorCompromisso = comandoSelecao.ExecuteReader();

            List<Compromisso> compromissos = new List<Compromisso>();

            while (leitorCompromisso.Read())
            {
                int idCompromisso = Convert.ToInt32(leitorCompromisso["ID"]);
                string assunto = Convert.ToString(leitorCompromisso["ASSUNTO"]);
                string local = Convert.ToString(leitorCompromisso["LOCAL"]);
                DateTime dataComromisso = Convert.ToDateTime(leitorCompromisso["DATACOMPROMISSO"]);
                string strHoraInicio = Convert.ToString(leitorCompromisso["HORAINICIO"]);
                string strHoraTermino = Convert.ToString(leitorCompromisso["HORATERMINO"]);
                string nomeContato = Convert.ToString(leitorCompromisso["NOME"]);

                TimeSpan horaInicio;
                TimeSpan.TryParse(strHoraInicio, out horaInicio);
                TimeSpan horaTermino;
                TimeSpan.TryParse(strHoraTermino, out horaTermino);
                Compromisso c = new Compromisso(nomeContato, assunto, local, dataComromisso, horaInicio, horaTermino);
                c.id = idCompromisso;

                compromissos.Add(c);
            }

            conexaoComBanco.Close();
            return compromissos;
        }

        public List<Compromisso> SelecionarCompromissosPorPeriodo(DateTime data1, DateTime data2)
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        F.ID,
                        F.ASSUNTO, 
		                F.LOCAL, 
		                F.DATACOMPROMISSO, 
		                F.HORAINICIO, 
		                F.HORATERMINO, 
		                C.NOME
                    FROM 
                        TBCOMPROMISSOS F LEFT JOIN
					    TBCONTATOS C
                    ON
                        F.ID_CONTATO = C.ID
                    WHERE
                        DATACOMPROMISSO
                    BETWEEN 
                        @DATA1 AND @DATA2";

            comandoSelecao.CommandText = sqlSelecao;

            comandoSelecao.Parameters.AddWithValue("DATA1", data1);
            comandoSelecao.Parameters.AddWithValue("DATA2", data2);

           SqlDataReader leitorCompromisso = comandoSelecao.ExecuteReader();

            List<Compromisso> compromissos = new List<Compromisso>();

            while (leitorCompromisso.Read())
            {
                int idCompromisso = Convert.ToInt32(leitorCompromisso["ID"]);
                string assunto = Convert.ToString(leitorCompromisso["ASSUNTO"]);
                string local = Convert.ToString(leitorCompromisso["LOCAL"]);
                DateTime dataComromisso = Convert.ToDateTime(leitorCompromisso["DATACOMPROMISSO"]);
                string strHoraInicio = Convert.ToString(leitorCompromisso["HORAINICIO"]);
                string strHoraTermino = Convert.ToString(leitorCompromisso["HORATERMINO"]);
                string nomeContato = Convert.ToString(leitorCompromisso["NOME"]);

                TimeSpan horaInicio;
                TimeSpan.TryParse(strHoraInicio, out horaInicio);
                TimeSpan horaTermino;
                TimeSpan.TryParse(strHoraTermino, out horaTermino);
                Compromisso c = new Compromisso(nomeContato, assunto, local, dataComromisso, horaInicio, horaTermino);
                c.id = idCompromisso;

                compromissos.Add(c);
            }

            conexaoComBanco.Close();
            return compromissos;
        }
    }
}
