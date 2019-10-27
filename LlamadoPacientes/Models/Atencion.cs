using System;

namespace LlamadoPacientes.Models
{
    public class Atencion
    {
        public int id { get; set; }

        public DateTime fechaAtencion { get; set; }

        public Paciente paciente { get; set; }

        public Box box { get; set; }

        public int estado { get; set; }

        public string nombreDoctor { get; set; }
    }
}