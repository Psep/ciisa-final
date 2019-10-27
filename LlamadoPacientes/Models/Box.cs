namespace LlamadoPacientes.Models
{
    public class Box
    {
        public Box()
        {

        }

        public Box(string nombre)
        {
            this.nombre = nombre;
        }

        public int numero { get; set; }
        public string nombre { get; set; }
    }
}