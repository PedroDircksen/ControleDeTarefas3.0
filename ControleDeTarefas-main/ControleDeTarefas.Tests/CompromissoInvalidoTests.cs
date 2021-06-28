using ControleDeTarefas.Domínio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleDeTarefas.Tests
{
    [TestClass]
    public class CompromissoInvalidoTests
    {
        public Compromisso compromisso;
        public CompromissoInvalidoTests()
        {
            TimeSpan horaInicio;
            TimeSpan.TryParse("17:00", out horaInicio);
            TimeSpan horaTermino;
            TimeSpan.TryParse("17:00", out horaTermino);
            compromisso = new Compromisso(1, "Abertura de Empresa", "Starbucks", new DateTime(2021, 06, 29), horaInicio, horaTermino);
        }

        [TestMethod]
        public void DeveRetornarHoraInvalida()
        {
            Assert.AreEqual("O horário de início não pode ser o mesmo que o de término\n", compromisso.validar());
        }

        [TestMethod]
        public void DeveRetornarHoraInvalida2()
        {
            TimeSpan horaInicio;
            TimeSpan.TryParse("18:00", out horaInicio);
            compromisso.horaInicio = horaInicio;
            Assert.AreEqual("O horário de início não pode ser maior que o de término\n", compromisso.validar());
        }

        [TestMethod]
        public void DeveRetornarDataNoFimDeSemana()
        {
            TimeSpan horaInicio;
            TimeSpan.TryParse("16:00", out horaInicio);
            compromisso.horaInicio = horaInicio;
            compromisso.dataCompromisso = new DateTime(2021, 06, 27);
            Assert.AreEqual("O compromisso não pode ser agendado em finais de semana\n", compromisso.validar());
        }

        [TestMethod]
        public void DeveRetornarHoraInvalidaEDataNoFimDeSemana()
        {
            compromisso.dataCompromisso = new DateTime(2021, 06, 27);
            Assert.AreEqual("O horário de início não pode ser o mesmo que o de término\n" +
                "O compromisso não pode ser agendado em finais de semana\n", compromisso.validar());
        }
    }
}
