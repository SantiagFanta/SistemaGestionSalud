using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionSalud.Models
{
    public class Turno
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public EstadoTurno Estado { get; set; }
        public string Motivo { get; set; }

        // Referencias a las otras clases
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; }

        public int IdMedico { get; set; }
        public Medico Medico { get; set; }

        public Turno(DateTime fechaHora, string motivo, Paciente paciente, Medico medico)
        {
            FechaHora = fechaHora;
            Motivo = motivo;
            Paciente = paciente;
            IdPaciente = paciente.Id;
            Medico = medico;
            IdMedico = medico.Id;
            Estado = EstadoTurno.Pendiente; // por defecto siempre arranca Pendiente
        }

        public override string ToString()
        {
            return $"[{Id}] {FechaHora:dd/MM/yyyy HH:mm} | " +
                   $"Paciente: {Paciente?.Apellido} | " +
                   $"Médico: Dr/a. {Medico?.Apellido} | " +
                   $"Estado: {Estado} | Motivo: {Motivo}";
        }
    }
}
