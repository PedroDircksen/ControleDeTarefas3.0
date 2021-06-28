using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas
{
    public class ControladorTarefa : Controlador<Tarefa>
    {
        public override void EditarRegistro(int idSelecionado, Tarefa tarefaAtualizada)
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoAtualizacao = new SqlCommand();
            comandoAtualizacao.Connection = conexaoComBanco;

            string sqlAtualizacao =
                @"UPDATE TBTAREFAS 
	                SET	
		                [PRIORIDADE] = @PRIORIDADE,
                        [TITULO] = @TITULO,
                        [DATACONCLUSAO] = @DATACONCLUSAO,
                        [PERCENTUALCONCLUIDO] = @PERCENTUALCONCLUIDO       
	                WHERE 
		                [ID] = @ID";

            comandoAtualizacao.CommandText = sqlAtualizacao;

            comandoAtualizacao.Parameters.AddWithValue("ID", idSelecionado);
            comandoAtualizacao.Parameters.AddWithValue("PRIORIDADE", tarefaAtualizada.prioridade);
            comandoAtualizacao.Parameters.AddWithValue("TITULO", tarefaAtualizada.titulo);
            comandoAtualizacao.Parameters.AddWithValue("DATACONCLUSAO", tarefaAtualizada.dataConclusao);
            comandoAtualizacao.Parameters.AddWithValue("PERCENTUALCONCLUIDO", tarefaAtualizada.percentualConcluido);

            comandoAtualizacao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }

        public void ConcluirRegistro(Tarefa tarefa)
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoAtualizacao = new SqlCommand();
            comandoAtualizacao.Connection = conexaoComBanco;

            string sqlAtualizacao =
                @"UPDATE TBTAREFAS 
	                SET	
		                [PRIORIDADE] = @PRIORIDADE,
                        [TITULO] = @TITULO,
                        [DATACONCLUSAO] = @DATACONCLUSAO,
                        [PERCENTUALCONCLUIDO] = @PERCENTUALCONCLUIDO       
	                WHERE 
		                [ID] = @ID";

            comandoAtualizacao.CommandText = sqlAtualizacao;

            comandoAtualizacao.Parameters.AddWithValue("ID", tarefa.id);
            comandoAtualizacao.Parameters.AddWithValue("PRIORIDADE", tarefa.prioridade);
            comandoAtualizacao.Parameters.AddWithValue("TITULO", tarefa.titulo);
            comandoAtualizacao.Parameters.AddWithValue("DATACONCLUSAO", DateTime.Now);
            comandoAtualizacao.Parameters.AddWithValue("PERCENTUALCONCLUIDO", 100);

            comandoAtualizacao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }

        public override void ExcluirRegistro(Tarefa tarefa)
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao =
                @"DELETE FROM TBTAREFAS 	                
	                WHERE 
		                [ID] = @ID";

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", tarefa.id);

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }

        public override void InserirNovo(Tarefa tarefa)
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

            string sqlInsercao =
                @"INSERT INTO TBTAREFAS
                    (
                        [PRIORIDADE],
                        [TITULO],
                        [DATACRIACAO],
                        [DATACONCLUSAO],
                        [PERCENTUALCONCLUIDO]                       
                    ) 
                    VALUES
                    (
                        @PRIORIDADE,
                        @TITULO,
                        @DATACRIACAO,
                        @DATACONCLUSAO,
                        @PERCENTUALCONCLUIDO
                    );";

            sqlInsercao +=
                @"SELECT SCOPE_IDENTITY();";

            comandoInsercao.CommandText = sqlInsercao;

            comandoInsercao.Parameters.AddWithValue("PRIORIDADE", tarefa.prioridade);
            comandoInsercao.Parameters.AddWithValue("TITULO", tarefa.titulo);
            comandoInsercao.Parameters.AddWithValue("DATACRIACAO", tarefa.dataCriacao);
            comandoInsercao.Parameters.AddWithValue("DATACONCLUSAO", tarefa.dataConclusao);
            comandoInsercao.Parameters.AddWithValue("PERCENTUALCONCLUIDO", tarefa.percentualConcluido);

            object id = comandoInsercao.ExecuteScalar();

            tarefa.id = Convert.ToInt32(id);

            conexaoComBanco.Close();
        }

        public List<Tarefa> SelecionarTodasTarefas()
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID],
                        [PRIORIDADE],
                        [TITULO],
                        [DATACRIACAO],
                        [DATACONCLUSAO],
                        [PERCENTUALCONCLUIDO]
                    FROM 
                        TBTAREFAS
                    ORDER BY 
                       CASE [PRIORIDADE]
                       WHEN 'Alta' THEN 1
                       WHEN 'Normal' THEN 2
                       WHEN 'Baixa' THEN 3
                    END";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefas = new List<Tarefa>();

            while (leitorTarefas.Read())
            {
                int id = Convert.ToInt32(leitorTarefas["ID"]);
                string strPrioridade = Convert.ToString(leitorTarefas["PRIORIDADE"]);
                string titulo = Convert.ToString(leitorTarefas["TITULO"]);
                DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);
                DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
                decimal percentualConcluido = Convert.ToDecimal(leitorTarefas["PERCENTUALCONCLUIDO"]);

                Tarefa t = new Tarefa(strPrioridade, titulo, dataCriacao, percentualConcluido);
                t.id = id;

                tarefas.Add(t);
            }

            conexaoComBanco.Close();
            return tarefas;
        }

        public override Tarefa SelecionarRegistroPorId(int id)
        {
            SqlConnection conexaoComBanco = AbrirConexaoComBancoDados();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID],
                        [PRIORIDADE],
                        [TITULO],
                        [DATACRIACAO],
                        [DATACONCLUSAO],
                        [PERCENTUALCONCLUIDO]
                    FROM 
                        TBTAREFAS
                    WHERE 
                        ID = @ID";

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", id);

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            if (leitorTarefas.Read() == null)
                return null;

            int idTarefa = Convert.ToInt32(leitorTarefas["ID"]);
            string strPrioridade = Convert.ToString(leitorTarefas["PRIORIDADE"]);
            string titulo = Convert.ToString(leitorTarefas["TITULO"]);
            DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);
            DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
            decimal percentualConcluido = Convert.ToDecimal(leitorTarefas["PERCENTUALCONCLUIDO"]);

            Tarefa tarefa = new Tarefa(strPrioridade, titulo, dataCriacao, percentualConcluido);
            tarefa.id = idTarefa;

            conexaoComBanco.Close();
            return tarefa;
        }
    }
}
