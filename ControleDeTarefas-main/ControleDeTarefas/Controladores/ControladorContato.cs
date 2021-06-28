using ControleDeTarefas.Domínio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleDeTarefas.Controladores
{
    public class ControladorContato : Controlador<Contato>
    {
        public override void EditarRegistro(int idSelecionado, Contato contato)
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoAtualizacao = new SqlCommand();
            comandoAtualizacao.Connection = conexaoComBanco;

            string sqlAtualizacao =
                @"UPDATE TBCONTATOS
	                SET	
		                [NOME] = @NOME,
                        [EMAIL] = @EMAIL,
                        [TELEFONE] = @TELEFONE,
                        [EMPRESA] = @EMPRESA,       
                        [CARGO] = @CARGO       
	                WHERE 
		                [ID] = @ID";

            comandoAtualizacao.CommandText = sqlAtualizacao;

            comandoAtualizacao.Parameters.AddWithValue("ID", idSelecionado);
            comandoAtualizacao.Parameters.AddWithValue("NOME", contato.nome);
            comandoAtualizacao.Parameters.AddWithValue("EMAIL", contato.email);
            comandoAtualizacao.Parameters.AddWithValue("TELEFONE", contato.telefone);
            comandoAtualizacao.Parameters.AddWithValue("EMPRESA", contato.empresa);
            comandoAtualizacao.Parameters.AddWithValue("CARGO", contato.cargo);

            comandoAtualizacao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }

        public override void ExcluirRegistro(Contato contato)
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao =
                @"DELETE FROM TBCONTATOS 	                
	                WHERE 
		                [ID] = @ID";

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", contato.id);

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }

        public override void InserirNovo(Contato contato)
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

            string sqlInsercao =
                @"INSERT INTO TBCONTATOS
                    (
                        [NOME],
                        [EMAIL],
                        [TELEFONE],
                        [EMPRESA],
                        [CARGO]                       
                    ) 
                    VALUES
                    (
                        @NOME,
                        @EMAIL,
                        @TELEFONE,
                        @EMPRESA,
                        @CARGO
                    )";

            sqlInsercao +=
                @"SELECT SCOPE_IDENTITY();";

            comandoInsercao.CommandText = sqlInsercao;

            comandoInsercao.Parameters.AddWithValue("NOME", contato.nome);
            comandoInsercao.Parameters.AddWithValue("EMAIL", contato.email);
            comandoInsercao.Parameters.AddWithValue("TELEFONE", contato.telefone);
            comandoInsercao.Parameters.AddWithValue("EMPRESA", contato.empresa);
            comandoInsercao.Parameters.AddWithValue("CARGO", contato.cargo);

            object id = comandoInsercao.ExecuteScalar();

            contato.id = Convert.ToInt32(id);

            conexaoComBanco.Close();
        }        

        public override Contato SelecionarRegistroPorId(int id)
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID],
                        [NOME],
                        [EMAIL],
                        [TELEFONE],
                        [EMPRESA],
                        [CARGO]    
                    FROM 
                        TBCONTATOS
                    WHERE 
                        ID = @ID";

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", id);

            SqlDataReader leitorContatos = comandoSelecao.ExecuteReader();

            if (leitorContatos.Read() == null)
                return null;

            int idContato = Convert.ToInt32(leitorContatos["ID"]);
            string nome = Convert.ToString(leitorContatos["NOME"]);
            string email = Convert.ToString(leitorContatos["EMAIL"]);
            string telefone = Convert.ToString(leitorContatos["TELEFONE"]);
            string empresa = Convert.ToString(leitorContatos["EMPRESA"]);
            string cargo = Convert.ToString(leitorContatos["CARGO"]);

            Contato contato = new Contato(nome, email, telefone, empresa, cargo);
            contato.id = idContato;

            conexaoComBanco.Close();
            return contato;
        }

        public List<Contato> SelecionarTodosContatos()
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID],
                        [NOME],
                        [EMAIL],
                        [TELEFONE],
                        [EMPRESA],
                        [CARGO]
                    FROM 
                        TBCONTATOS
                    ORDER BY [CARGO]";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorContatos = comandoSelecao.ExecuteReader();

            List<Contato> contatos = new List<Contato>();

            while (leitorContatos.Read())
            {
                int idContato = Convert.ToInt32(leitorContatos["ID"]);
                string nome = Convert.ToString(leitorContatos["NOME"]);
                string email = Convert.ToString(leitorContatos["EMAIL"]);
                string telefone = Convert.ToString(leitorContatos["TELEFONE"]);
                string empresa = Convert.ToString(leitorContatos["EMPRESA"]);
                string cargo = Convert.ToString(leitorContatos["CARGO"]);

                Contato c = new Contato(nome, email, telefone, empresa, cargo);
                c.id = idContato;

                contatos.Add(c);
            }

            conexaoComBanco.Close();
            return contatos;
        }       
        
    }
}
