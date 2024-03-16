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
            InitializeComponent(); // Inicializa el programa.
            cargarTabla(null); // Llama a la función CargarTabla.
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            // Zona donde se carga y muestra la tabla con la información de la base de datos. NO BORRAR!!
        }

        // Crear y guardar nuevos registros
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Convierte la información ingresada en el formulario a texto plano para facilitar su uso.
                String Codigo = txtCodigo.Text;
                String Marca = txtMarca.Text;
                String Modelo = txtModelo.Text;
                int Año = int.Parse(txtAno.Text);
                String Color = txtColor.Text;
                String Descripcion = txtDescripcion.Text;
                int Existencias = int.Parse(txtExistencias.Text);
                double Precio = double.Parse(txtPrecio.Text);

                // Verifica que todos los campos del formulario tengan datos correctos.
                if (Codigo != "" && Marca != "" && Modelo != "" && Año >1900 && Color != "" && Descripcion != "" && Existencias > 0 && Precio > 0)
                {

                    // Inserta la información ingresada en el formulario en los campos correspondientes de la Base de Datos.
                    string sql = "INSERT INTO autos (Codigo, Marca, Modelo, Año, Color, Descripcion, Existencias, Precio) VALUES ('" + Codigo + "', '" + Marca + "', '" + Modelo + "', '" + Año + "', '" + Color + "', '" + Descripcion + "', '" + Existencias + "', '" + Precio + "')";

                    MySqlConnection conexionBD = Conexion.conexion(); // Llama a la función de Conexión a la Base de Datos.
                    conexionBD.Open(); // Abre la conexión con la Base de Datos.

                    try
                    {
                        MySqlCommand comando = new MySqlCommand(sql, conexionBD); // Inicia una instancia de SQL sobre la Base de Datos.
                        comando.ExecuteNonQuery(); // Verifica que las columnas de la Base de Datos se hayan actualizado con los datos insertados en la declaración sobre la instancia anterior.
                        MessageBox.Show("Registro guardado"); // Mensaje de guardado exitoso.
                        limpiar(); // Llama a la función Limpiar para vaciar el formulario.
                    }
                    catch (MySqlException ex) // Informa si existe algún error en la conexión con la Base de Datos.
                    {
                        MessageBox.Show("Error al guardar: " + ex.Message); // Mensaje de error al guardar.
                    }
                    finally
                    {
                        conexionBD.Close(); // Cierra la conexión con la Base de Datos.
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos"); // Mensaje de error si algún campo del formulario está vacío.
                }
            }
            catch(FormatException fex) // Funciones a realizar si no se cumplen los parámetros extablecidos en el formulario.
            {
                MessageBox.Show("Datos inválidos: " + fex.Message); // Mensaje de error si se introducen datos no validos en el formulario.
            }
        }

        // Buscar registro por código
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text; // Convierte la información ingresada en el formulario a texto plano.
            MySqlDataReader reader = null; // Solicita la lectura de información de la Base de Datos.

            // Solicita un solo registro de la Base de Datos que coincida con el código ingresado en el campo de búsqueda del formulario.
            string sql = "SELECT Id, Codigo, Marca, Modelo, Año, Color, Descripcion, Existencias, Precio FROM autos WHERE Codigo LIKE '" + codigo + "' LIMIT 1";
            MySqlConnection conexionBD = Conexion.conexion(); // Llama a la función de Conexión a la Base de Datos.
            conexionBD.Open(); // Abre la conexión con la Base de Datos.

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD); // Inicia una instancia de SQL sobre la Base de Datos.
                reader = comando.ExecuteReader(); // Se realiza una lectura de datos de la Base de Datos sobre la instancia anterior.
                if(reader.HasRows) // Si encuentra filas con la información deseada.
                {
                    while (reader.Read()) // Lee y obtiene toda la información de los campos seleccionados de la fila donde se encontró la información buscada.
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
                else // Si no encuentra la información deseada.
                {
                    MessageBox.Show("No se encontraron registros"); // Mensaje al usuario al no encontrar registros.
                }
            }
            catch (MySqlException ex) // Informa si existe algún error en la conexión con la Base de Datos.
            {
                MessageBox.Show("Error al buscar: " + ex.Message); // Mensaje al usuario en caso de error en la búsqueda.
            }
            finally // Termina la función.
            {
                conexionBD.Close(); // Cierra la conexión con la Base de Datos.
            }
        }

        // Actualizar registro
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // Convierte la información ingresada en el formulario a texto plano para facilitar su uso.
            String Id = txtId.Text;
            String Codigo = txtCodigo.Text;
            String Marca = txtMarca.Text;
            String Modelo = txtModelo.Text;
            int Año = int.Parse(txtAno.Text);
            String Color = txtColor.Text;
            String Descripcion = txtDescripcion.Text;
            int Existencias = int.Parse(txtExistencias.Text);
            double Precio = double.Parse(txtPrecio.Text);

            // Actualiza la información modificada en el formulario en los campos correspondientes de la Base de Datos.
            string sql = "UPDATE autos SET Codigo='" + Codigo + "', Marca='" + Marca + "', Modelo='" + Modelo + "', Año='" + Año + "', Color='" + Color + "', Descripcion='" + Descripcion + "', Existencias='" + Existencias + "', Precio='" + Precio + "' WHERE Id='" + Id + "'";

            MySqlConnection conexionBD = Conexion.conexion(); // Llama a la función de Conexión a la Base de Datos.
            conexionBD.Open(); // Abre la conexión con la Base de Datos.

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD); // Inicia una instancia de SQL sobre la Base de Datos.
                comando.ExecuteNonQuery(); // Verifica que las columnas de la Base de Datos se hayan actualizado con los datos insertados en la declaración sobre la instancia anterior.
                MessageBox.Show("Registro actualizado"); // Mensaje de Actualización exitosa.
                limpiar(); // Llama a la función Limpiar para vaciar el formulario.
            }
            catch (MySqlException ex) // Informa si existe algún error en la conexión con la Base de Datos.
            {
                MessageBox.Show("Error al actualizar: " + ex.Message); // Mensaje de error al actualizar la Base de Datos.
            }
            finally
            {
                conexionBD.Close(); // Cierra la conexión con la Base de Datos.
            }
        }

        // Eliminar registro
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            String Id = txtId.Text; // Convierte la información ingresada en el formulario a texto plano para facilitar su uso.

            string sql = "DELETE FROM autos WHERE Id='" + Id + "'"; // Elimina el registro de la Base de Datos que coincida con el Id seleccionado.

            MySqlConnection conexionBD = Conexion.conexion(); // Llama a la función de Conexión a la Base de Datos.
            conexionBD.Open(); // Abre la conexión con la Base de Datos.

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD); // Inicia una instancia de SQL sobre la Base de Datos.
                comando.ExecuteNonQuery(); // Verifica que las columnas de la Base de Datos se hayan actualizado con los datos insertados en la declaración sobre la instancia anterior.
                MessageBox.Show("Registro eliminado"); // Mensaje de Eliminación exitosa.
                limpiar(); // Llama a la función Limpiar para vaciar el formulario.
            }
            catch (MySqlException ex) // Informa si existe algún error en la conexión con la Base de Datos.
            {
                MessageBox.Show("Error al eliminar: " + ex.Message); // Mensaje de error al actualizar la Base de Datos.
            }
            finally
            {
                conexionBD.Close(); // Cierra la conexión con la Base de Datos.
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

        // Función CargarTabla. Carga y muestra información de la Base de Datos.
        private void cargarTabla(string dato)
        {
            List<Autos> lista = new List<Autos>(); // Carga una nueva instancia de Autos.cs para cargar la información de la base de datos de forma ordenada.
            CtrlAutos _ctrlAutos = new CtrlAutos(); // Carga una nueva instancia de CtrlAutos.cs para realizar la consulta en la información obtenida.
            dataGridView1.DataSource = _ctrlAutos.consulta(dato); // Carga y muestra la información obtenida en forma de tabla.
        }
    }
}
