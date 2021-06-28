using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.Domínio
{
    public class Contato : EntidadeBase, IValidavel
    {
        public string nome;
        public string email;
        public string telefone;
        public string empresa;
        public string cargo;

        public Contato(string nome, string email, string empresa, string telefone, string cargo)
        {
            this.nome = nome;
            this.email = email;
            this.empresa = empresa;
            this.telefone = telefone;
            this.cargo = cargo;
        }

        public string validar()
        {
            string resultadoValidacao = "";

            if (!email.Contains("@") || !email.Contains(".com"))
                resultadoValidacao += "Email inválido \n";
            
            if (telefone.Length < 9)
                resultadoValidacao += "O telefone precisa ter mais de 9 dígitos \n";
            
            if (telefone.Length > 15)
                resultadoValidacao += "O telefone não pode ter mais de 15 dígitos \n";

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;
        }
    }
}
