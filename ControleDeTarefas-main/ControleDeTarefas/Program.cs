using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas
{
    class Program
    {
        static TelaPrincipal telaPrincipal = new TelaPrincipal();

        static void Main(string[] args)
        {
            while (true)
            {
                TelaBase telaSelecionada = telaPrincipal.ObterTela();

                if (telaSelecionada == null)
                    break;

                Console.Clear();

                if (telaSelecionada is TelaBase)
                    Console.WriteLine(telaSelecionada.Titulo); Console.WriteLine();

                string opcao = telaSelecionada.ObterOpcao();

                if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    continue;
                
                if(telaSelecionada is IConcluivel)
                {
                    IConcluivel tela = (IConcluivel)telaSelecionada;

                    if (opcao == "1")
                        tela.InserirNovoRegistro();

                    else if (opcao == "2")
                        tela.ConcluirRegistro();

                    else if (opcao == "3")
                    {
                        bool temRegistros = tela.VisualizarRegistrosSeparados(TipoVisualizacao.VisualizandoTela);
                        if (temRegistros)
                            Console.ReadLine();
                    }

                    else if (opcao == "4")
                        tela.EditarRegistro();

                    else if (opcao == "5")
                        tela.ExcluirRegistro();
                }
                else if (telaSelecionada is ICadastravel)
                {
                    ICadastravel tela = (ICadastravel)telaSelecionada;

                    if (opcao == "1")
                        tela.InserirNovoRegistro();

                    else if (opcao == "2")
                    {
                        bool temRegistros = tela.VisualizarRegistros(TipoVisualizacao.VisualizandoTela);
                        if (temRegistros)
                            Console.ReadLine();
                    }

                    else if (opcao == "3")
                        tela.EditarRegistro();

                    else if (opcao == "4")
                        tela.ExcluirRegistro();
                }                
            }
        }
    }
}
