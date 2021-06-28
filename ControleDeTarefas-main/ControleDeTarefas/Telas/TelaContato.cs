using ControleDeTarefas.Controladores;
using ControleDeTarefas.Domínio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.Telas
{
    class TelaContato : TelaCadastro<Contato>, ICadastravel
    {
        readonly ControladorContato controlador;

        public TelaContato(ControladorContato control) : base("cadastro de contatos", control)
        {
            controlador = control;
        }

        public override Contato ObterObjeto(TipoAcao tipoAcao)
        {
            Console.Write("Digite o nome do colega: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o email do colega: ");
            string email = Console.ReadLine();

            Console.Write("Digite o telefone do colega: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite a empresa do colega: ");
            string empresa = Console.ReadLine();

            Console.Write("Digite o cargo do colega: ");
            string cargo = Console.ReadLine();

            return new Contato(nome, email, telefone, empresa, cargo);
        }

        public bool VisualizarRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela("Visualizando contatos...");

            List<Contato> contatos = controlador.SelecionarTodosContatos();

            if (contatos.Count == 0)
            {
                ApresentarMensagem("Não há nenhum contato registrado", TipoMensagem.Atencao);
                return false;
            }

            string configuracaColunasTabela = "{0,-5} | {1,-25} | {2,-25} | {3,-25} | {4,-25} | {5,-25}";
            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Nome", "Email", "Telefone", "Empresa", "Cargo");

            foreach (var contato in contatos)
            {
                Console.WriteLine(configuracaColunasTabela, contato.id, contato.nome,
                        contato.email, contato.telefone, contato.empresa, contato.cargo);
            }

            return true;
        }

        public override bool VisualizarTodosRegistros(TipoVisualizacao tipo)
        {
            VisualizarRegistros(TipoVisualizacao.Pesquisando);
            return true;
        }
    }
}
