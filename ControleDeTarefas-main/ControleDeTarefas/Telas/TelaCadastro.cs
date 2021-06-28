using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas
{
    public abstract class TelaCadastro<T> : TelaBase where T : EntidadeBase, IValidavel
    {
        private readonly Controlador<T> controlador;

        public TelaCadastro(string tit, Controlador<T> control) : base(tit)
        {
            controlador = control;
        }

        public abstract T ObterObjeto(TipoAcao tipoAcao);

        public abstract bool VisualizarTodosRegistros(TipoVisualizacao tipo);

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo uma novo registro...");

            T registro = ObterObjeto(TipoAcao.Inserindo);

            string resultadoValidacao = registro.validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                controlador.InserirNovo(registro);
                ApresentarMensagem("Registro inserido com sucesso", TipoMensagem.Sucesso);
            }
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando um registro...");

            bool temRegistros = VisualizarTodosRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do registro que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            T registroEncontrada = controlador.SelecionarRegistroPorId(id);
            if (registroEncontrada == null)
            {
                ApresentarMensagem("Nenhum registro foi encontrado com este número: " + id, TipoMensagem.Erro);
                EditarRegistro();
                return;
            }

            T registro = ObterObjeto(TipoAcao.Editando);            

            string resultadoValidacao = registro.validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                controlador.EditarRegistro(registroEncontrada.id, registro);
                ApresentarMensagem("Registro editado com sucesso", TipoMensagem.Sucesso);
            }
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um registro...");

            bool temRegistros = VisualizarTodosRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do registro que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            T registro = controlador.SelecionarRegistroPorId(id);

            if (registro == null)
            {
                ApresentarMensagem("Nenhum registro foi encontrado com este número: " + id, TipoMensagem.Erro);
                ExcluirRegistro();
                return;
            }

            controlador.ExcluirRegistro(registro);
            bool conseguiuExcluir = true;

            if (conseguiuExcluir)
                ApresentarMensagem("Registro excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o registro", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }
    }
}
