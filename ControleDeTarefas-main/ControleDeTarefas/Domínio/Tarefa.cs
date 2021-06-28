using System;
using System.Collections.Generic;

namespace ControleDeTarefas
{
    public class Tarefa : EntidadeBase, IValidavel
    {
        public string prioridade;
        public string titulo;
        public DateTime dataCriacao;
        public DateTime dataConclusao = DateTime.Now;
        public decimal percentualConcluido;
        public Dictionary<int, string> prioridades = new Dictionary<int, string>();

        public Tarefa(int i,string t, DateTime dCriacao, decimal percentual)
        {
            prioridade = GerarPrioridade(i);
            titulo = t;
            dataCriacao = dCriacao;
            percentualConcluido = percentual;
        }

        public Tarefa(string prioridade, string titulo, DateTime dataCriacao, decimal percentualConcluido)
        {
            this.prioridade = prioridade;
            this.titulo = titulo;
            this.dataCriacao = dataCriacao;
            this.percentualConcluido = percentualConcluido;
        }

        public string GerarPrioridade(int p)
        {
                prioridades.Add(1, "Baixa");
                prioridades.Add(2, "Normal");
                prioridades.Add(3, "Alta");

            for (int i = 0; i < 3; i++)
            {
                if (prioridades.ContainsKey(p))
                    return prioridades[p];
            }
            return "";
        }

        public string validar()
        {
                string resultadoValidacao = "";

                if (percentualConcluido > 100)
                    resultadoValidacao += "O percentual não pode ser maior que 100\n";

                if (percentualConcluido < 0)
                    resultadoValidacao += "O percentual não pode ser menor que 0\n";

                if (string.IsNullOrEmpty(resultadoValidacao))
                    resultadoValidacao = "ESTA_VALIDO";

                return resultadoValidacao;
            
        }
    }
}
