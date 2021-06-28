using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas
{
    interface ICadastravel
    {
        void InserirNovoRegistro();

        void EditarRegistro();

        void ExcluirRegistro();

        bool VisualizarRegistros(TipoVisualizacao tipo);

        string ObterOpcao();
    }
}
