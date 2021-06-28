using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleDeTarefas.Tests
{
    [TestClass]
    public class TarefaTests 
    {
        public Tarefa tarefa;
        public TarefaTests()
        {
            tarefa = new Tarefa(3,"Lavar a louça", DateTime.Now, 50);
        }

        [TestMethod]
        public void DeveValidarTarefa()
        {
            Assert.AreEqual("ESTA_VALIDO", tarefa.validar());
        }
    }
}
