using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Los_Santos___Autos_usados
{
    internal class Conexion
    {
        // Inicia la conexión a la Base de Datos.
        public static MySqlConnection conexion()
        {
            // Datos de acceso.
            string servidor = "mx66.hostgator.mx";
            string bd = "elprofea_LosSantos";
            string usuario = "elprofea_Pruebas";
            string password = "Contraseña";

            // Se envían los datos de conexión para su verificación.
            string cadenaConexion = "Database=" + bd + "; Data Source=" + servidor + "; User Id=" + usuario + "; Password=" + password + "";

            try
            {
                MySqlConnection conexionBD = new MySqlConnection(cadenaConexion); // Se solicita la conexión.

                return conexionBD; // Conexión exitosa.
            }
            catch (MySqlException ex) // Informa si existe algún error en la conexión.
            {
                Console.WriteLine("Error: " + ex.Message); // Mensaje de consola en caso de error en la conexión.
                return null;
            }
        }
    }
}
