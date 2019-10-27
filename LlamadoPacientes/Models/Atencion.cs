using System;

namespace LlamadoPacientes.Models
{
    public class Atencion
    {
        public int idAtencion { get; set; }

        public DateTime fechaAtencion { get; set; }

        public Paciente paciente { get; set; }

        public Box box { get; set; }

        public string estado { get; set; }
    }
}