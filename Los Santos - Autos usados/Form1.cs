using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Los_Santos___Autos_usados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cargarTabla(null);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        // Crear y guardar nuevos registros
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                String Codigo = txtCodigo.Text;
                String Marca = txtMarca.Text;
                String Modelo = txtModelo.Text;
                int Año = int.Parse(txtAno.Text);
                String Color = txtColor.Text;
                String Descripcion = txtDescripcion.Text;
                int Existencias = int.Parse(txtExistencias.Text);
                double Precio = double.Parse(txtPrecio.Text);

                if (Codigo != "" && Marca != "" && Modelo != "" && Año >1900 && Color != "" && Descripcion != "" && Existencias > 0 && Precio > 0)
                {

                    string sql = "INSERT INTO autos (Codigo, Marca, Modelo, Año, Color, Descripcion, Existencias, Precio) VALUES ('" + Codigo + "', '" + Marca + "', '" + Modelo + "', '" + Año + "', '" + Color + "', '" + Descripcion + "', '" + Existencias + "', '" + Precio + "')";

                    MySqlConnection conexionBD = Conexion.conexion();
                    conexionBD.Open();

                    try
                    {
                        MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Registro guardado");
                        limpiar(); // Llama a la función Limpiar para vaciar el formulario.
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error al guardar: " + ex.Message);
                    }
                    finally
                    {
                        conexionBD.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos");
                }
            }
            catch(FormatException fex)
            {
                MessageBox.Show("Datos inválidos: " + fex.Message);
            }
        }

        // Buscar registro por código
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text;
            MySqlDataReader reader = null;

            string sql = "SELECT Id, Codigo, Marca, Modelo, Año, Color, Descripcion, Existencias, Precio FROM autos WHERE Codigo LIKE '" + codigo + "' LIMIT 1";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtId.Text = reader.GetString(0);
                        txtCodigo.Text = reader.GetString(1);
                        txtMarca.Text = reader.GetString(2);
                        txtModelo.Text = reader.GetString(3);
                        txtAno.Text = reader.GetString(4);
                        txtColor.Text = reader.GetString(5);
                        txtDescripcion.Text = reader.GetString(6);
                        txtExistencias.Text = reader.GetString(7);
                        txtPrecio.Text = reader.GetString(8);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        // Actualizar registro
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            String Id = txtId.Text;
            String Codigo = txtCodigo.Text;
            String Marca = txtMarca.Text;
            String Modelo = txtModelo.Text;
            int Año = int.Parse(txtAno.Text);
            String Color = txtColor.Text;
            String Descripcion = txtDescripcion.Text;
            int Existencias = int.Parse(txtExistencias.Text);
            double Precio = double.Parse(txtPrecio.Text);

            string sql = "UPDATE autos SET Codigo='" + Codigo + "', Marca='" + Marca + "', Modelo='" + Modelo + "', Año='" + Año + "', Color='" + Color + "', Descripcion='" + Descripcion + "', Existencias='" + Existencias + "', Precio='" + Precio + "' WHERE Id='" + Id + "'";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro actualizado");
                limpiar(); // Llama a la función Limpiar para vaciar el formulario.
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        // Eliminar registro
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            String Id = txtId.Text;

            string sql = "DELETE FROM autos WHERE Id='" + Id + "'";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro eliminado");
                limpiar(); // Llama a la función Limpiar para vaciar el formulario.
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        // Botón Limpiar.
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar(); // Llama a la función Limpiar para vaciar el formulario.
        }

        // Función Limpiar. Vacía los valores de los campos del formulario.
        private void limpiar()
        {
            txtId.Text = "";
            txtCodigo.Text = "";
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtAno.Text = "";
            txtColor.Text = "";
            txtDescripcion.Text = "";
            txtExistencias.Text = "";
            txtPrecio.Text = "";

        }

        // Buscar registro por palabras clave
        private void btnBuscarTabla_Click(object sender, EventArgs e)
        {
            string dato = txtCampo.Text; // Convierte la información ingresada por el usuario en texto plano.
            cargarTabla(dato); // Llama a la función CargarTabla.
        }

        // Función CargarTabla. Carga y muestra información de la Base de Datos que coincida con la busqueda.
        private void cargarTabla(string dato)
        {
            List<Autos> lista = new List<Autos>(); // Carga una nueva instancia de Autos.cs para cargar la información de la base de datos de forma ordenada.
            CtrlAutos _ctrlAutos = new CtrlAutos(); // Carga una nueva instancia de CtrlAutos.cs para realizar la consulta en la información obtenida.
            dataGridView1.DataSource = _ctrlAutos.consulta(dato); // Carga y muestra la información obtenida en forma de tabla.
        }
    }
}
