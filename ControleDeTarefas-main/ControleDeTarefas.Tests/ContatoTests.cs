using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControleDeTarefas.Domínio;
using ControleDeTarefas.Controladores;

namespace ControleDeTarefas.Tests
{
    [TestClass]
    public class ContatoTests
    {
        public ControladorContato controlador;
        public Contato contato;
        public ContatoTests()
        {
            controlador = new ControladorContato();
            contato = new Contato("Pedro", "dircksenpedro@gmail.com", "NDD", "49998085663", "Programador");
        }

        [TestMethod]
        public void DeveValidarContato()
        {
            Assert.AreEqual("ESTA_VALIDO",contato.validar());
        }

    }
}
