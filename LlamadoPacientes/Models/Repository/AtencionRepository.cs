using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LlamadoPacientes.Models.Repository
{
    public class AtencionRepository
    {
        private string ConnStr = ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString;

        private List<Atencion> FindList(int? lastId, string storedProcedure)
        {
            SqlConnection conn = this.GetConnection();
            SqlCommand cmd = new SqlCommand(storedProcedure, connection: conn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (lastId != null)
            {
                cmd.Parameters.Add("@lastIdAtencion", SqlDbType.Int).Value = lastId;
            }

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Atencion> atenciones = new List<Atencion>();

            while (reader.Read())
            {
                Atencion atencion = new Atencion();
                atencion.id = reader.GetInt32(0);

                Box box = new Box(this.GetString(reader, 1));
                atencion.box = box;

                atencion.nombreDoctor = this.GetString(reader, 2);

                Paciente paciente = new Paciente(this.GetString(reader, 3));
                atencion.paciente = paciente;

                atenciones.Add(atencion);
            }

            this.CloseAll(conn, reader: reader);

            return atenciones;
        }

        public List<Atencion> FindCarrusel(int? lastId)
        {
            return this.FindList(lastId, "sp_list_carrusel");
        }

        public List<Atencion> FindAtenciones()
        {
            return this.FindList(null, "sp_list_ultimosllamados");
        }

        public void Update(int idAtencion)
        {
            SqlConnection conn = this.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_update_estado_atendido", connection: conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@idAtencion", SqlDbType.Int).Value = idAtencion;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }                                                         

        private SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString: ConnStr);
            return conn;
        }

        private void CloseAll(SqlConnection conn, SqlDataReader reader)
        {
            if (conn != null)
            {
                conn.Close();
            }

            if (reader != null)
            {
                reader.Close();
            }
        }

        private string GetString(SqlDataReader reader, int position)
        {
            if (!reader.IsDBNull(position))
            {
                return reader.GetString(position);
            }
            else
            {
                return null;
            }
        }
    }
}