using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;
using SistemaGestionSalud.Models;

namespace SistemaGestionSalud.Data
{
    public class PacienteRepository
    {
        public void Agregar(Paciente paciente)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                con.Open();
                string query = @"INSERT INTO Pacientes (Nombre, Apellido, DNI, FechaNacimiento, Telefono, Email)
                                 VALUES (@Nombre, @Apellido, @DNI, @FechaNacimiento, @Telefono, @Email)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", paciente.Apellido);
                cmd.Parameters.AddWithValue("@DNI", paciente.DNI);
                cmd.Parameters.AddWithValue("@FechaNacimiento", paciente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Telefono", paciente.Telefono);
                cmd.Parameters.AddWithValue("@Email", paciente.Email);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Paciente> ObtenerTodos()
        {
            List<Paciente> lista = new List<Paciente>();

            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                con.Open();
                string query = "SELECT * FROM Pacientes";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Paciente p = new Paciente(
                        reader["Nombre"].ToString(),
                        reader["Apellido"].ToString(),
                        reader["DNI"].ToString(),
                        Convert.ToDateTime(reader["FechaNacimiento"]),
                        reader["Telefono"].ToString(),
                        reader["Email"].ToString()
                    );
                    p.Id = Convert.ToInt32(reader["Id"]);
                    lista.Add(p);
                }
            }

            return lista;
        }
    }
}