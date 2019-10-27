using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de RequestLlamado
/// </summary>
public class RequestLlamado
{
    public RequestLlamado()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DateTime fecha { set; get; }
    public string nombreDoctor { set; get; }
    public int box { set; get; }
    public string nombrePaciente { set; get; }
    public string codigoPaciente { set; get; }
}