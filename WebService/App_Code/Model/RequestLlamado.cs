using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

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

    private DateTime? _fecha;
    public string nombreDoctor { set; get; }
    public int box { set; get; }
    public string nombrePaciente { set; get; }
    public string codigoPaciente { set; get; }

    // https://stackoverflow.com/questions/2861779/xmlserializer-the-string-is-not-a-valid-allxsd-value
    [XmlElement("fecha")]
    public string FechaString
    {
        get
        {
            return _fecha.HasValue ? XmlConvert.ToString(_fecha.Value, XmlDateTimeSerializationMode.Unspecified) : string.Empty;
        }
        set { _fecha = Convert.ToDateTime(value); }
    }

    [XmlIgnore]
    public DateTime? fecha
    {
        get { return _fecha; }
        set { _fecha = value; }
    }

    public override string ToString() => "[Llamado = fecha: " + this.fecha + ", doc: " 
        + this.nombreDoctor + ", box: " + this.box + ", codpaciente: " + this.codigoPaciente
        + ", nombrepaciente: " + this.nombrePaciente + "]";
}