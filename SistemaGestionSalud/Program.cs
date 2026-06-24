using SistemaGestionSalud.Data;
using SistemaGestionSalud.Models;

namespace SistemaGestionSalud
{
    internal class Program
    {
        static PacienteRepository pacienteRepo = new PacienteRepository();
        static MedicoRepository medicoRepo = new MedicoRepository();
        static TurnoRepository turnoRepo = new TurnoRepository();

        static List<Paciente> pacientes = new List<Paciente>();
        static List<Medico> medicos = new List<Medico>();
        static List<Turno> turnos = new List<Turno>();

        static void Main(string[] args)
        {
            // Cargamos los datos de la base de datos al iniciar
            pacientes = pacienteRepo.ObtenerTodos();
            medicos = medicoRepo.ObtenerTodos();
            turnos = turnoRepo.ObtenerTodos(pacientes, medicos);

            Console.WriteLine("Bienvenido al Sistema de Gestión de Salud");

            bool continuar = true;
            while (continuar)
            {
                MostrarMenuPrincipal();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": MenuPacientes(); break;
                    case "2": MenuMedicos(); break;
                    case "3": MenuTurnos(); break;
                    case "0":
                        continuar = false;
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }

        static void MostrarMenuPrincipal()
        {
            Console.WriteLine("\n===== MENÚ PRINCIPAL =====");
            Console.WriteLine("1. Gestión de Pacientes");
            Console.WriteLine("2. Gestión de Médicos");
            Console.WriteLine("3. Gestión de Turnos");
            Console.WriteLine("0. Salir");
            Console.Write("Elegí una opción: ");
        }

        static void MenuPacientes()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\n--- GESTIÓN DE PACIENTES ---");
                Console.WriteLine("1. Agregar paciente");
                Console.WriteLine("2. Listar pacientes");
                Console.WriteLine("0. Volver");
                Console.Write("Elegí una opción: ");

                switch (Console.ReadLine())
                {
                    case "1": AgregarPaciente(); break;
                    case "2": ListarPacientes(); break;
                    case "0": continuar = false; break;
                    default: Console.WriteLine("Opción inválida."); break;
                }
            }
        }

        static void AgregarPaciente()
        {
            Console.WriteLine("\n-- Nuevo Paciente --");

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();

            Console.Write("DNI: ");
            string dni = Console.ReadLine();

            Console.Write("Fecha de nacimiento (dd/mm/aaaa): ");
            DateTime fechaNac;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy",
                   null, System.Globalization.DateTimeStyles.None, out fechaNac))
            {
                Console.Write("Formato inválido. Ingresá dd/mm/aaaa: ");
            }

            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Paciente nuevo = new Paciente(nombre, apellido, dni, fechaNac, telefono, email);
            pacienteRepo.Agregar(nuevo);
            pacientes = pacienteRepo.ObtenerTodos();

            Console.WriteLine($"\n✓ Paciente {nombre} {apellido} agregado correctamente.");
        }

        static void ListarPacientes()
        {
            if (pacientes.Count == 0)
            {
                Console.WriteLine("\nNo hay pacientes registrados.");
                return;
            }

            Console.WriteLine("\n-- Listado de Pacientes --");
            foreach (Paciente p in pacientes)
                Console.WriteLine(p);
        }

        static void MenuMedicos()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\n--- GESTIÓN DE MÉDICOS ---");
                Console.WriteLine("1. Agregar médico");
                Console.WriteLine("2. Listar médicos");
                Console.WriteLine("0. Volver");
                Console.Write("Elegí una opción: ");

                switch (Console.ReadLine())
                {
                    case "1": AgregarMedico(); break;
                    case "2": ListarMedicos(); break;
                    case "0": continuar = false; break;
                    default: Console.WriteLine("Opción inválida."); break;
                }
            }
        }

        static void AgregarMedico()
        {
            Console.WriteLine("\n-- Nuevo Médico --");

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();

            Console.Write("Especialidad: ");
            string especialidad = Console.ReadLine();

            Console.Write("Matrícula: ");
            string matricula = Console.ReadLine();

            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine();

            Medico nuevo = new Medico(nombre, apellido, especialidad, matricula, telefono);
            medicoRepo.Agregar(nuevo);
            medicos = medicoRepo.ObtenerTodos();

            Console.WriteLine($"\n✓ Médico {nombre} {apellido} agregado correctamente.");
        }

        static void ListarMedicos()
        {
            if (medicos.Count == 0)
            {
                Console.WriteLine("\nNo hay médicos registrados.");
                return;
            }

            Console.WriteLine("\n-- Listado de Médicos --");
            foreach (Medico m in medicos)
                Console.WriteLine(m);
        }

        static void MenuTurnos()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\n--- GESTIÓN DE TURNOS ---");
                Console.WriteLine("1. Agregar turno");
                Console.WriteLine("2. Listar turnos");
                Console.WriteLine("3. Cancelar turno");
                Console.WriteLine("0. Volver");
                Console.Write("Elegí una opción: ");

                switch (Console.ReadLine())
                {
                    case "1": AgregarTurno(); break;
                    case "2": ListarTurnos(); break;
                    case "3": CancelarTurno(); break;
                    case "0": continuar = false; break;
                    default: Console.WriteLine("Opción inválida."); break;
                }
            }
        }

        static void AgregarTurno()
        {
            if (pacientes.Count == 0 || medicos.Count == 0)
            {
                Console.WriteLine("\nNecesitás tener al menos un paciente y un médico registrado.");
                return;
            }

            Console.WriteLine("\n-- Nuevo Turno --");

            Console.WriteLine("\nPacientes disponibles:");
            foreach (Paciente p in pacientes)
                Console.WriteLine(p);

            Console.Write("Ingresá el ID del paciente: ");
            int idPaciente;
            while (!int.TryParse(Console.ReadLine(), out idPaciente) ||
                   pacientes.Find(p => p.Id == idPaciente) == null)
            {
                Console.Write("ID inválido. Intentá de nuevo: ");
            }
            Paciente paciente = pacientes.Find(p => p.Id == idPaciente);

            Console.WriteLine("\nMédicos disponibles:");
            foreach (Medico m in medicos)
                Console.WriteLine(m);

            Console.Write("Ingresá el ID del médico: ");
            int idMedico;
            while (!int.TryParse(Console.ReadLine(), out idMedico) ||
                   medicos.Find(m => m.Id == idMedico) == null)
            {
                Console.Write("ID inválido. Intentá de nuevo: ");
            }
            Medico medico = medicos.Find(m => m.Id == idMedico);

            Console.Write("\nFecha y hora del turno (dd/mm/aaaa hh:mm): ");
            DateTime fechaHora;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm",
                   null, System.Globalization.DateTimeStyles.None, out fechaHora))
            {
                Console.Write("Formato inválido. Ingresá dd/mm/aaaa hh:mm: ");
            }

            if (turnoRepo.ExisteConflicto(idMedico, fechaHora))
            {
                Console.WriteLine("\n✗ El médico ya tiene un turno en ese horario.");
                return;
            }

            Console.Write("Motivo de la consulta: ");
            string motivo = Console.ReadLine();

            Turno nuevo = new Turno(fechaHora, motivo, paciente, medico);
            turnoRepo.Agregar(nuevo);
            turnos = turnoRepo.ObtenerTodos(pacientes, medicos);

            Console.WriteLine($"\n✓ Turno registrado correctamente.");
        }

        static void ListarTurnos()
        {
            if (turnos.Count == 0)
            {
                Console.WriteLine("\nNo hay turnos registrados.");
                return;
            }

            Console.WriteLine("\n-- Listado de Turnos --");
            foreach (Turno t in turnos)
                Console.WriteLine(t);
        }

        static void CancelarTurno()
        {
            if (turnos.Count == 0)
            {
                Console.WriteLine("\nNo hay turnos registrados.");
                return;
            }

            ListarTurnos();

            Console.Write("\nIngresá el ID del turno a cancelar: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id) ||
                   turnos.Find(t => t.Id == id) == null)
            {
                Console.Write("ID inválido. Intentá de nuevo: ");
            }

            Turno turno = turnos.Find(t => t.Id == id);

            if (turno.Estado == EstadoTurno.Cancelado)
            {
                Console.WriteLine("\nEse turno ya está cancelado.");
                return;
            }

            turnoRepo.CancelarTurno(id);
            turnos = turnoRepo.ObtenerTodos(pacientes, medicos);
            Console.WriteLine("\n✓ Turno cancelado correctamente.");
        }
    }
}