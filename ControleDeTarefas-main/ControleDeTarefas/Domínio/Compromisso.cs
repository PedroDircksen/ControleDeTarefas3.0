using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.Domínio
{
    public class Compromisso : EntidadeBase, IValidavel
    {
        public string assunto;
        public string local;
        public string nomeContato;
        public DateTime dataCompromisso;
        public TimeSpan horaInicio;
        public TimeSpan horaTermino;
        public int idContato;
        public bool ehPresencial;

        public Compromisso(int idContato, string assunto, string local, DateTime dataCompromisso, TimeSpan horaInicio, TimeSpan horaTermino)
        {
            this.idContato = idContato;
            this.assunto = assunto;
            this.local = local;
            this.dataCompromisso = dataCompromisso;
            this.horaInicio = horaInicio;
            this.horaTermino = horaTermino;
        }

        public Compromisso(string nomeContato, string assunto, string local, DateTime dataCompromisso, TimeSpan horaInicio, TimeSpan horaTermino)
        {
            this.nomeContato = nomeContato;
            this.assunto = assunto;
            this.local = local;
            this.dataCompromisso = dataCompromisso;
            this.horaInicio = horaInicio;
            this.horaTermino = horaTermino;
        }

        public string validar()
        {
            string resultadoValidacao = "";

            if (this.horaInicio == this.horaTermino)
                resultadoValidacao += "O horário de início não pode ser o mesmo que o de término\n";

            if (this.horaInicio > this.horaTermino)
                resultadoValidacao += "O horário de início não pode ser maior que o de término\n";

            if (this.dataCompromisso.DayOfWeek.ToString() == "Saturday" || this.dataCompromisso.DayOfWeek.ToString() == "Sunday")
                resultadoValidacao += "O compromisso não pode ser agendado em finais de semana\n";

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;
        }
    }
}
