using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;

namespace SistemaGestionSalud.Data
{
    public class ConexionDB
    {
        private static string _cadenaConexion =
            "Server=localhost\\SQLEXPRESS;Database=ClinicaDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_cadenaConexion);
        }
    }
}