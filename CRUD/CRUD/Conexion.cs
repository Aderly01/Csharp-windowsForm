using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    internal class Conexion
    {
        public static MySqlConnection conexion()
        {
            string server = "localhost";
            string db = "tienda";
            string user = "root";
            string password = "00001111";

            string cadenaConexion = $"Database={db}; Data Source={server};" +
                $"User Id={user};Password={password}";

            try
            {
                MySqlConnection conexioDB = new MySqlConnection(cadenaConexion);
                return conexioDB;
            }
            catch(MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}
