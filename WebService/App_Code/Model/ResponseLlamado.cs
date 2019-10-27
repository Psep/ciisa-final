using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ResponseLlamado
/// </summary>
public class ResponseLlamado
{
    public ResponseLlamado()
    {
       
    }

    public ResponseLlamado(int codigo, string mensaje)
    {
        this.codigo = codigo;
        this.mensaje = mensaje;
    }

    public static ResponseLlamado ResponseOk()
    {
        return new ResponseLlamado(0, "Transacción exitosa");
    }

    public static ResponseLlamado ResponseError()
    {
        return new ResponseLlamado(1, "Error al guardar");
    }

    public int codigo { set; get; }
    public string mensaje { set; get; }
}