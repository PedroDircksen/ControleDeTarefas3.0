namespace ControleDeTarefas
{
    internal interface IConcluivel
    {
        void InserirNovoRegistro();

        void EditarRegistro();

        void ExcluirRegistro();

        bool VisualizarTodosRegistros(TipoVisualizacao tipo);

        string ObterOpcao();

        void ConcluirRegistro();

        bool VisualizarRegistrosSeparados(TipoVisualizacao tipo);
    }
}