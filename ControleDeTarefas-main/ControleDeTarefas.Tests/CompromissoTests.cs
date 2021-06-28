using ControleDeTarefas.Controladores;
using ControleDeTarefas.Domínio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleDeTarefas.Tests
{
    [TestClass]
    public class CompromissoTests
    {
        public Compromisso compromisso;
        public CompromissoTests()
        {
            TimeSpan horaInicio;
            TimeSpan.TryParse("16:00", out horaInicio);
            TimeSpan horaTermino;
            TimeSpan.TryParse("17:00", out horaTermino);
            compromisso = new Compromisso(1, "Abertura de Empresa", "Starbucks", new DateTime(2021, 06, 29), horaInicio, horaTermino);
        }
        [TestMethod]
        public void DeveRetornarHoraInvalida()
        {
            Assert.AreEqual("ESTA_VALIDO", compromisso.validar());
        }
    }
}
