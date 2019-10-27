using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de AtencionRepository
/// </summary>
public class AtencionRepository
{
    private string ConnStr = ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString;

    public ResponseLlamado Create(RequestLlamado llamado)
    {
        try
        {
            SqlConnection conn = this.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_insertar_atencion", connection: conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@fechaAtencion", SqlDbType.Date).Value = llamado.fecha;
            cmd.Parameters.Add("@nombreDoctor", SqlDbType.VarChar).Value = llamado.nombreDoctor;
            cmd.Parameters.Add("@box", SqlDbType.Int).Value = llamado.box;
            cmd.Parameters.Add("@nombrePaciente", SqlDbType.VarChar).Value = llamado.nombrePaciente;
            cmd.Parameters.Add("@codigoPaciente", SqlDbType.VarChar).Value = llamado.codigoPaciente;

            conn.Open();
            int execute = cmd.ExecuteNonQuery();
            conn.Close();

            if (execute > 0)
            {
                return ResponseLlamado.ResponseOk();
            } else
            {
                return ResponseLlamado.ResponseError();
            }
        } catch (SqlException e)
        {
            return new ResponseLlamado(e.ErrorCode, mensaje: e.Message);
        }
    }

    private SqlConnection GetConnection()
    {
        SqlConnection conn = new SqlConnection(connectionString: ConnStr);
        return conn;
    }
}