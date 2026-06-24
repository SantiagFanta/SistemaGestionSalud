using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionSalud.Models
{
        public class Medico
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Especialidad { get; set; }
            public string Matricula { get; set; }
            public string Telefono { get; set; }

            public Medico(string nombre, string apellido, string especialidad,
                          string matricula, string telefono)
            {
                Nombre = nombre;
                Apellido = apellido;
                Especialidad = especialidad;
                Matricula = matricula;
                Telefono = telefono;
            }

            public override string ToString()
            {
                return $"[{Id}] Dr/a. {Apellido}, {Nombre} | {Especialidad} | Matrícula: {Matricula}";
            }
        }
 }
