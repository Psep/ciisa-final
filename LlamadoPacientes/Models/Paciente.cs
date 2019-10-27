namespace LlamadoPacientes.Models
{
    public class Paciente
    {
        public Paciente() { }

        public Paciente(string codigo)
        {
            this.codigo = codigo;
        }

        public string codigo { get; set; }
        public string nombreCompleto { get; set; }
    }
}