using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas
{
    public class TelaTarefa : TelaCadastro<Tarefa>, IConcluivel
    {
        readonly ControladorTarefa controlador;
        public TelaTarefa(ControladorTarefa control) : base("cadastro de tarefas", control)
        {
            controlador = control;
        }

        public override string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir nova tarefa");
            Console.WriteLine("Digite 2 para concluir uma tarefa");
            Console.WriteLine("Digite 3 para visualizar tarefas");
            Console.WriteLine("Digite 4 para editar uma tarefa");
            Console.WriteLine("Digite 5 para excluir uma tarefa");

            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }

        public void ConcluirRegistro()
        {
            ConfigurarTela("Concluindo uma tarefa...");

            bool temRegistros = VisualizarTodosRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número da tarefa que deseja concluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Tarefa tarefaEncontrada = controlador.SelecionarRegistroPorId(id);
            if (tarefaEncontrada == null)
            {
                ApresentarMensagem("Nenhuma tarefa foi encontrada com este número: " + id, TipoMensagem.Erro);
                ConcluirRegistro();
                return;
            }

            try
            {
                controlador.ConcluirRegistro(tarefaEncontrada);
            }
            catch
            {
                ApresentarMensagem("ERRO: não foi possível concluir esta tarefa", TipoMensagem.Erro);
                return;
            }

            ApresentarMensagem("Sucesso ao concluir sua tarefa", TipoMensagem.Sucesso);
        }

        public override Tarefa ObterObjeto(TipoAcao tipoAcao)
        {
            Console.WriteLine("Digite o grau de prioridade da tarefa");
            Console.WriteLine("\n1 - para Baixa");
            Console.WriteLine("2 - para Normal");
            Console.WriteLine("3 - para Alta");
            int grauPrioridade = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o título da tarefa: ");
            string titulo = Console.ReadLine();

            DateTime dataCriacao = new DateTime();
            if (tipoAcao == TipoAcao.Inserindo)
            {
                Console.Write("Digite a data de criação da tarefa: ");
                dataCriacao = Convert.ToDateTime(Console.ReadLine());
            }

            Console.Write("Digite o percentual concluído da tarefa: ");
            decimal percentualConcluido = Convert.ToDecimal(Console.ReadLine());

            return new Tarefa(grauPrioridade, titulo, dataCriacao, percentualConcluido);
        }

        public override bool VisualizarTodosRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela("Visualizando tarefas...");

            List<Tarefa> tarefas = controlador.SelecionarTodasTarefas();

            if (tarefas.Count == 0)
            {
                ApresentarMensagem("Não há nenhuma tarefa registrada", TipoMensagem.Atencao);
                return false;
            }

            string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-25} | {3,-25}";
            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Nível de prioridade", "Titulo", "Data de criação");

            foreach (var tarefa in tarefas)
            {
                Console.WriteLine(configuracaColunasTabela, tarefa.id, tarefa.prioridade,
                        tarefa.titulo, tarefa.dataCriacao.ToString("dd/MM/yyyy"));
            }

            return true;
        }

        public bool VisualizarRegistrosSeparados(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela("Visualizando tarefas...");

            List<Tarefa> tarefas = controlador.SelecionarTodasTarefas();
            List<Tarefa> tarefasConcluidas = new List<Tarefa>();
            List<Tarefa> tarefasEmAberto = new List<Tarefa>();

            foreach (var tarefa in tarefas)
            {
                if (tarefa.percentualConcluido == 100)
                    tarefasConcluidas.Add(tarefa);

                else if (tarefa.percentualConcluido < 100)
                    tarefasEmAberto.Add(tarefa);
            }

            Console.WriteLine("Digite 1 para visualizar tarefas em aberto");
            Console.WriteLine("Digite 2 para visualizar tarefas concluidas");
            Console.Write("\nOpção: ");

            string opcaoEscolhida = Console.ReadLine();

            if (opcaoEscolhida != "1" && opcaoEscolhida != "2")
            {
                Console.Write("Opção inválida, tente novamente");
                VisualizarRegistrosSeparados(tipo);
            }

            string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-25} | {3,-25}";

            if (opcaoEscolhida == "1")
            {
                if (tarefasEmAberto.Count == 0)
                    ApresentarMensagem("Nenhuma tarefa em aberto cadastrada", TipoMensagem.Atencao);
                else
                    ApresentarTarefas(tarefasEmAberto, configuracaColunasTabela);

            }

            if (opcaoEscolhida == "2")
            {
                if (tarefasConcluidas.Count == 0)
                    ApresentarMensagem("Nenhuma tarefa concluida", TipoMensagem.Atencao);
                else
                    ApresentarTarefas(tarefasConcluidas, configuracaColunasTabela);
            }

            return true;
        }

        #region Métodos privados
        private void ApresentarTarefas(List<Tarefa> tarefas, string configuracaColunasTabela)
        {
            Console.Clear();
            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Nível de prioridade", "Titulo", "Data de criação");

            foreach (Tarefa tarefa in tarefas)
            {
                Console.WriteLine(configuracaColunasTabela, tarefa.id, tarefa.prioridade,
                    tarefa.titulo, tarefa.dataCriacao.ToString("dd/MM/yyyy"));
            }            
        }
        #endregion
    }
}
