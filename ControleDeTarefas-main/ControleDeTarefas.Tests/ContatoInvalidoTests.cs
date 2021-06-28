using ControleDeTarefas.Controladores;
using ControleDeTarefas.Domínio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleDeTarefas.Tests
{
    [TestClass]
    public class ContatoInvalidoTests
    {
        public ControladorContato controlador;
        public Contato contato;
        public ContatoInvalidoTests()
        {
            controlador = new ControladorContato();
            contato = new Contato("Pedro", "dircksenpedro@gmail", "NDD", "49998085663", "Programador");
        }

        [TestMethod]
        public void DeveRetornarEmailInvalido()
        {
            Assert.AreEqual("Email inválido \n", contato.validar());
        }

        [TestMethod]
        public void DeveRetornarEmailETelefoneInvalido()
        {
            contato.telefone = "98085663";
            Assert.AreEqual("Email inválido \nO telefone precisa ter mais de 9 dígitos \n", contato.validar());
        }

        [TestMethod]
        public void DeveRetornarEmailETelefoneInvalido2()
        {
            contato.telefone = "55+ (49)99808-5663";
            Assert.AreEqual("Email inválido \nO telefone não pode ter mais de 15 dígitos \n", contato.validar());
        }
    }
}
