using ControleDeTarefas.Controladores;
using ControleDeTarefas.Domínio;
using System;
using System.Collections.Generic;

namespace ControleDeTarefas.Telas
{
    class TelaCompromisso : TelaCadastro<Compromisso>, ICadastravel
    {
        private readonly ControladorCompromisso controlador;
        private readonly ControladorContato controladorContato;
        private readonly TelaContato telaContato;
        public TelaCompromisso(Controlador<Compromisso> control) : base("Cadastro de Compromissos", control)
        {
            controlador = new ControladorCompromisso();
            controladorContato = new ControladorContato();
            telaContato = new TelaContato(controladorContato);
        }

        public override Compromisso ObterObjeto(TipoAcao tipoAcao)
        {
            telaContato.VisualizarRegistros(TipoVisualizacao.VisualizandoTela);

            Console.Write("\nCaso deseje vincular esse compromisso à um contato informe o id, caso contrário digite 0: ");
            int idContato = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            Console.Write("Informe o assunto da reunião: ");
            string assunto = Console.ReadLine();

            string local;
            bool ehPresencial;

            GetLocalOuLinkDoCompromisso(tipoAcao, out local, out ehPresencial);

            Console.Write("\nInforme a data da reunião: ");
            DateTime dataReuniao = Convert.ToDateTime(Console.ReadLine());

            Console.Write("\nInforme a hora de início da reunião: ");
            string strHoraInicio = Convert.ToString(Console.ReadLine());

            Console.Write("\nInforme a hora de término da reunião: ");
            string strHoraTermino = Convert.ToString(Console.ReadLine());

            TimeSpan horaInicio;
            TimeSpan.TryParse(strHoraInicio, out horaInicio);
            TimeSpan horaTermino;
            TimeSpan.TryParse(strHoraTermino, out horaTermino);
            Compromisso compromisso = new Compromisso(idContato, assunto, local, dataReuniao, horaInicio, horaTermino);
            compromisso.ehPresencial = ehPresencial;

            return compromisso;
        }
        
        public override bool VisualizarTodosRegistros(TipoVisualizacao tipo)
        {
            List<Compromisso> compromissos = controlador.SelecionarTodosCompromissos();
            List<Compromisso> compromissosEmAberto = new List<Compromisso>();

            if (compromissos.Count == 0)
            {
                ApresentarMensagem("Não há nenhum compromisso registrado", TipoMensagem.Atencao);
                return false;
            }

            foreach (var compromisso in compromissos)
            {
                if (compromisso.dataCompromisso > DateTime.Now)
                    compromissosEmAberto.Add(compromisso);
            }

            string configuracaColunasTabela = "{0,-5} | {1,-25} | {2,-25} | {3,-25} | {4,-25} | {5,-25} | {6,-25}";
            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Assunto", "Local ou Link", "data", "Hora Início", "Hora Término", "Nome do Contato");

            foreach (var compromisso in compromissos)
            {
                Console.WriteLine(configuracaColunasTabela, compromisso.id, compromisso.assunto,
                        compromisso.local, compromisso.dataCompromisso.ToString("dd/MM/yyyy"), compromisso.horaInicio, compromisso.horaTermino, compromisso.nomeContato);
            }

            return true;
        }

        public bool VisualizarRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela("Visualizando Compromissos...");

            string configuracaColunasTabela = "{0,-5} | {1,-25} | {2,-25} | {3,-25} | {4,-25} | {5,-25} | {6,-25}";  

            Console.WriteLine("Digite 1 para visualizar compromissos passados");
            Console.WriteLine("Digite 2 para visualizar compromissos em aberto");
            Console.Write("\nOpção: ");
            
            string opcaoEscolhida = Console.ReadLine();
            List<Compromisso> compromissos;
            List<Compromisso> compromissosPassados = new List<Compromisso>();
            List<Compromisso> compromissosEmAberto = new List<Compromisso>();

            if (opcaoEscolhida != "1" && opcaoEscolhida != "2")
            {               
                Console.Write("Opção inválida, tente novamente");
                VisualizarRegistros(tipo);
            }

            if (opcaoEscolhida == "1")
            {
                compromissos = controlador.SelecionarTodosCompromissos();
                foreach (var compromisso in compromissos)
                {
                    if (compromisso.dataCompromisso < DateTime.Now)
                        compromissosPassados.Add(compromisso);
                }
                
                if (compromissosPassados.Count == 0)
                    ApresentarMensagem("Nenhum compromisso passado cadastrado", TipoMensagem.Atencao);
                else
                    apresentarCompromissos(compromissosPassados, configuracaColunasTabela);
            }

            if (opcaoEscolhida == "2")
            {
                Console.Clear();
                Console.Write("Informe até que dia deseja ver seus compromissos: ");
                DateTime dataDeParada = Convert.ToDateTime(Console.ReadLine());

                compromissosEmAberto = controlador.SelecionarCompromissosPorPeriodo(DateTime.Now, dataDeParada); 

                if (compromissosEmAberto.Count == 0)
                    ApresentarMensagem("Nenhum compromisso em aberto", TipoMensagem.Atencao);
                else
                    apresentarCompromissos(compromissosEmAberto, configuracaColunasTabela);
            } 
            return true;
        }                
        
        private void apresentarCompromissos(List<Compromisso> compromissos, string configuracaColunasTabela)
        {
            Console.Clear();
            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Assunto", "Local ou Link", "data", "Hora Início", "Hora Término", "Nome do Contato");

            foreach(var compromisso in compromissos)
            {
                Console.WriteLine(configuracaColunasTabela, compromisso.id, compromisso.assunto,
                        compromisso.local, compromisso.dataCompromisso.ToString("dd/MM/yyyy"), compromisso.horaInicio, compromisso.horaTermino, compromisso.nomeContato);
            }
        }
        
        private void GetLocalOuLinkDoCompromisso(TipoAcao tipoAcao, out string local, out bool ehPresencial)
        {
            Console.WriteLine("\nDigite 1 para reunião presencial");
            Console.WriteLine("Digite 2 para reunião remota");
            Console.Write("Opção: ");
            string opcaoLocal = Console.ReadLine();

            local = "";
            ehPresencial = false;
            if (opcaoLocal == "1")
            {
                ehPresencial = true;
                Console.Write("\nInforme o local da reunião: ");
                local = Console.ReadLine();
            }
            else if (opcaoLocal == "2")
            {
                ehPresencial = false;
                Console.Write("\nInforme o link da reunião: ");
                local = Console.ReadLine();
            }
            else
            {
                ApresentarMensagem("\nInforme uma opção válida",TipoMensagem.Atencao);
                ObterObjeto(tipoAcao);
            }
        }
    }
}
