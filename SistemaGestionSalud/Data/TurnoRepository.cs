using Microsoft.Data.SqlClient;
using SistemaGestionSalud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

using Microsoft.Data.SqlClient;
using SistemaGestionSalud.Models;

namespace SistemaGestionSalud.Data
{
    public class TurnoRepository
    {
        public void Agregar(Turno turno)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                con.Open();
                string query = @"INSERT INTO Turnos (FechaHora, Estado, Motivo, IdPaciente, IdMedico)
                                 VALUES (@FechaHora, @Estado, @Motivo, @IdPaciente, @IdMedico)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FechaHora", turno.FechaHora);
                cmd.Parameters.AddWithValue("@Estado", turno.Estado.ToString());
                cmd.Parameters.AddWithValue("@Motivo", turno.Motivo);
                cmd.Parameters.AddWithValue("@IdPaciente", turno.IdPaciente);
                cmd.Parameters.AddWithValue("@IdMedico", turno.IdMedico);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Turno> ObtenerTodos(List<Paciente> pacientes, List<Medico> medicos)
        {
            List<Turno> lista = new List<Turno>();

            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                con.Open();
                string query = "SELECT * FROM Turnos";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int idPaciente = Convert.ToInt32(reader["IdPaciente"]);
                    int idMedico = Convert.ToInt32(reader["IdMedico"]);

                    Paciente paciente = pacientes.Find(p => p.Id == idPaciente);
                    Medico medico = medicos.Find(m => m.Id == idMedico);

                    Turno t = new Turno(
                        Convert.ToDateTime(reader["FechaHora"]),
                        reader["Motivo"].ToString(),
                        paciente,
                        medico
                    );
                    t.Id = Convert.ToInt32(reader["Id"]);
                    t.Estado = Enum.Parse<EstadoTurno>(reader["Estado"].ToString());
                    lista.Add(t);
                }
            }

            return lista;
        }

        public void CancelarTurno(int id)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                con.Open();
                string query = "UPDATE Turnos SET Estado = 'Cancelado' WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public bool ExisteConflicto(int idMedico, DateTime fechaHora)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                con.Open();
                string query = @"SELECT COUNT(*) FROM Turnos 
                                 WHERE IdMedico = @IdMedico 
                                 AND FechaHora = @FechaHora 
                                 AND Estado != 'Cancelado'";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdMedico", idMedico);
                cmd.Parameters.AddWithValue("@FechaHora", fechaHora);

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
    }
}