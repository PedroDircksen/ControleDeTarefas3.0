using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas
{
    public abstract class Controlador<T> where T : EntidadeBase
    {
        public abstract void InserirNovo(T registro);

        public abstract T SelecionarRegistroPorId(int id);
            
        public abstract void EditarRegistro(int id, T registro);

        public abstract void ExcluirRegistro(T registro);

        public static SqlConnection AbrirConexaoComBancoDados()
        {
            string enderecoDBTarefa =
                           @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefa;Integrated Security=True;Pooling=False";

            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefa;
            conexaoComBanco.Open();
            return conexaoComBanco;
        }
    }
}
