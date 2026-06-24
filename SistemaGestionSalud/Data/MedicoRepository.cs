using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;
using SistemaGestionSalud.Models;

namespace SistemaGestionSalud.Data
{
    public class MedicoRepository
    {
        public void Agregar(Medico medico)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                con.Open();
                string query = @"INSERT INTO Medicos (Nombre, Apellido, Especialidad, Matricula, Telefono)
                                 VALUES (@Nombre, @Apellido, @Especialidad, @Matricula, @Telefono)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", medico.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", medico.Apellido);
                cmd.Parameters.AddWithValue("@Especialidad", medico.Especialidad);
                cmd.Parameters.AddWithValue("@Matricula", medico.Matricula);
                cmd.Parameters.AddWithValue("@Telefono", medico.Telefono);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Medico> ObtenerTodos()
        {
            List<Medico> lista = new List<Medico>();

            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                con.Open();
                string query = "SELECT * FROM Medicos";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Medico m = new Medico(
                        reader["Nombre"].ToString(),
                        reader["Apellido"].ToString(),
                        reader["Especialidad"].ToString(),
                        reader["Matricula"].ToString(),
                        reader["Telefono"].ToString()
                    );
                    m.Id = Convert.ToInt32(reader["Id"]);
                    lista.Add(m);
                }
            }

            return lista;
        }
    }
}