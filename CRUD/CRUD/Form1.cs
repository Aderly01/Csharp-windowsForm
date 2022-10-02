using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                int codigo = Int32.Parse(txtCodigo.Text);
                String nombre = txtNombre.Text;
                String descripcion = txtDescripcion.Text;
                double precio = Double.Parse(txtPrecio.Text);
                int existencia = Int32.Parse(txtExistencias.Text);

                if (codigo != null && nombre != "" && descripcion != "" && precio > 0 && existencia > 0)
                {

                    string sql = $"INSERT INTO productos" +
                        "(codigo,nombre,descripcion,precio_publico,existencias)" +
                        $"VALUES ({codigo},'{nombre}','{descripcion}',{precio},{existencia})";

                    MySqlConnection conexionDB = Conexion.conexion();
                    conexionDB.Open();

                    try
                    {
                        MySqlCommand comando = new MySqlCommand(sql, conexionDB);
                        comando.ExecuteNonQuery();
                        MessageBox.Show("*****Registro Guardado*****");
                        limpiar();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error al guardar: " + ex.Message);
                    }
                    finally
                    {
                        conexionDB.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos");
                }
            } catch(FormatException fex)
            {
                MessageBox.Show("Datos incorrectos: " + fex.Message); 
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            String id = txtId.Text;
            int codigo = Int32.Parse(txtCodigo.Text);
            String nombre = txtNombre.Text;
            String descripcion = txtDescripcion.Text;
            double precio = Double.Parse(txtPrecio.Text);
            int existencia = Int32.Parse(txtExistencias.Text);

            string sql = $"UPDATE productos " +
                $"SET codigo={codigo},nombre='{nombre}'," +
                $"descripcion='{descripcion}',precio_publico={precio},existencias={existencia} " +
                $"WHERE id={id}"; 
                
            MySqlConnection conexionDB = Conexion.conexion();
            conexionDB.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionDB);
                comando.ExecuteNonQuery();
                MessageBox.Show("*****Registro Modificado*****");
                limpiar();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
            finally
            {
                conexionDB.Close();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            String codigo = txtCodigo.Text;
            MySqlDataReader reader = null;

            String sql = $"SELECT * FROM productos WHERE codigo LIKE '{codigo}' LIMIT 1";
            MySqlConnection conexionDB = Conexion.conexion();
            conexionDB.Open();

            try
            {
                MySqlCommand commando = new MySqlCommand(sql,conexionDB);
                reader = commando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtId.Text = reader.GetString(0);
                        txtNombre.Text = reader.GetString(1);
                        txtNombre.Text = reader.GetString(2);
                        txtDescripcion.Text = reader.GetString(3);
                        txtPrecio.Text = reader.GetString(4);
                        txtExistencias.Text = reader.GetString(5);
                    }
                }
                else
                {
                    MessageBox.Show("*****No se encontraron registros*****");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al buscar: "+ex.Number);
            }
            finally
            {
                conexionDB.Close();
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            String id = txtId.Text;

            string sql = $"DELETE FROM productos WHERE id='{id}'";

            MySqlConnection conexionDB = Conexion.conexion();
            conexionDB.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionDB);
                comando.ExecuteNonQuery();
                MessageBox.Show("*****Registro Eliminado*****");
                limpiar();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
            finally
            {
                conexionDB.Close();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        private void limpiar()
        {
            txtId.Text = "";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtExistencias.Text = "";

        }
    }
}
