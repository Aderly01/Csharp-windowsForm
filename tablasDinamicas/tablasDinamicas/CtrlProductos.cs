using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablasDinamicas
{
    internal class CtrlProductos : Conexion
    {
        public List<Object> consulta(string dato)
        {
            MySqlDataReader reader; 
            List<Object> lista = new List<Object>();

            string sql;
            if(dato == null)
            {
                sql = "SELECT id,codigo,nombre,descripcion,precio_publico,existencias FROM productos ORDER BY nombre ASC";
            } else
            {
                sql = $"SELECT id,codigo,nombre,descripcion,precio_publico,existencias FROM productos WHERE codigo LIKE '%{dato}%' OR " +
                    $"nombre LIKE '%{dato}%' OR " +
                    $"descripcion LIKE '%{dato}%' OR " +
                    $"precio_publico LIKE '%{dato}%' OR " +
                    $"existencias LIKE '%{dato}%' ORDER BY nombre ASC";
            }
            try
            {
                MySqlConnection conexionDB = base.conexion();
                conexionDB.Open();
                MySqlCommand comando = new MySqlCommand(sql, conexionDB);

                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Productos _producto = new Productos();
                    _producto.Id = Int32.Parse(reader.GetString(0));
                    _producto.Codigo = reader[1].ToString();
                    _producto.Nombre = reader.GetString("nombre");
                    _producto.Descripcion = reader[3].ToString();
                    _producto.Precio_publico = Double.Parse(reader.GetString(4));
                    _producto.Existencias = Int32.Parse(reader.GetString(5));

                    lista.Add(_producto);
                }
            } catch (MySqlException e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            return lista;
        }
    }
}
