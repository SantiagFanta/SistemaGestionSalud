using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionSalud.Models
{
    public class Paciente
    {
        // Propiedades
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        // Constructor
        public Paciente(string nombre, string apellido, string dni,
                        DateTime fechaNacimiento, string telefono, string email)
        {
            Nombre = nombre;
            Apellido = apellido;
            DNI = dni;
            FechaNacimiento = fechaNacimiento;
            Telefono = telefono;
            Email = email;
        }

        // Método para calcular la edad automáticamente
        public int ObtenerEdad()
        {
            int edad = DateTime.Today.Year - FechaNacimiento.Year;
            if (FechaNacimiento.Date > DateTime.Today.AddYears(-edad))
                edad--;
            return edad;
        }

        // ToString para mostrar los datos
        public override string ToString()
        {
            return $"[{Id}] {Apellido}, {Nombre} | DNI: {DNI} | Edad: {ObtenerEdad()} años | Tel: {Telefono}";
        }
    }
}

