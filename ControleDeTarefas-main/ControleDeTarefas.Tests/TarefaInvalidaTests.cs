using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleDeTarefas.Tests
{
    [TestClass]
    public class TarefaInvalidaTests
    {
        public Tarefa tarefa;
        public TarefaInvalidaTests()
        {
            tarefa = new Tarefa(3, "Lavar a louça", DateTime.Now, -1);
        }

        [TestMethod]
        public void DeveRetornarPercentualInvalido()
        {
            Assert.AreEqual("O percentual não pode ser menor que 0\n", tarefa.validar());
        }

        [TestMethod]
        public void DeveRetornarPercentualInvalido2()
        {
            tarefa.percentualConcluido = 101;
            Assert.AreEqual("O percentual não pode ser maior que 100\n", tarefa.validar());
        }
    }
}
