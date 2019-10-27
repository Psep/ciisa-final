using System.Collections.Generic;

namespace LlamadoPacientes.Models.Repository
{
    public class AtencionRepository
    {
        private List<Atencion> generarListaRandom(int tamanio)
        {
            List<Atencion> listAtencion = new List<Atencion>();

            for (int i = 0; i < tamanio; i++)
            {
                Atencion atencion = new Atencion();
                atencion.box = new Box();
                atencion.box.numero = i + 1;
                atencion.box.profesional = new Profesional();
                atencion.box.profesional.nombre = "Dr. Juan" + i;
                atencion.paciente = new Paciente();
                atencion.paciente.codigo = i + 1;
                atencion.paciente.nombre = "Pepito" + i;
                listAtencion.Add(atencion);
            }

            return listAtencion;
        }

        public List<Atencion> listarCarrusel(int posicion)
        {
            List<Atencion> lista = this.generarListaRandom(10);

            switch (posicion)
            {
                case 1:
                    return lista.FindAll(x => x.box.numero <= 4);

                case 2:
                    return lista.FindAll(x => x.box.numero > 4 && x.box.numero <= 8);

                default:
                    return lista.FindAll(x => x.box.numero > 8);
            }
        }

        public List<Atencion> listarUltimasAtenciones()
        {
            List<Atencion> lista = this.generarListaRandom(10);
            return lista.FindAll(x => x.box.numero > 7);
        }

    }
}