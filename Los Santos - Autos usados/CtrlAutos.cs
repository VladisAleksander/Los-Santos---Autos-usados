using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Los_Santos___Autos_usados
{
    internal class CtrlAutos
    {
        public List<Object> consulta(string dato)
        {
            MySqlDataReader reader; // Solicita la lectura de información de la Base de Datos.
            List<Object> lista = new List<Object>(); // Solicita una lista de información de la Base de Datos.
            string sql;

            if(dato == null) // Si los datos existen.
            {
                // Selecciona los campos requeridos de la Base de Datos y los ordena por Marca.
                sql = "SELECT Id, Codigo, Marca, Modelo, Año, Color, Descripcion, Existencias, Precio FROM autos ORDER BY Marca ASC";
            }
            else
            {
                // Selecciona los campos requeridos que contengan información similar.
                sql = "SELECT Id, Codigo, Marca, Modelo, Año, Color, Descripcion, Existencias, Precio FROM autos WHERE Codigo LIKE '%" + dato + "%' OR Marca LIKE '%" + dato + "%' OR Modelo LIKE '%" + dato + "%' OR Año LIKE '%" + dato + "%' OR Color LIKE '%" + dato + "%' OR Descripcion LIKE '%" + dato + "%' OR Existencias LIKE '%" + dato + "%' OR Precio LIKE '%" + dato + "%' ORDER BY Marca ASC";
            }

            try
            {
                MySqlConnection conexionBD = Conexion.conexion(); // Llama a la función de Conexión a la Base de Datos.
                conexionBD.Open(); // Abre la conexión con la Base de Datos.
                MySqlCommand comando = new MySqlCommand(sql, conexionBD); // Inicia una instancia de SQL sobre la Base de Datos.
                reader = comando.ExecuteReader(); // Se realiza una lectura de datos de la Base de Datos sobre la instancia anterior.

                while (reader.Read()) // Lee y obtiene toda la información de los campos seleccionados de la fila donde se encontró la información buscada.
                {
                    Autos _autos = new Autos();
                    _autos.Id = int.Parse(reader.GetString(0));
                    _autos.Codigo = reader.GetString(1);
                    _autos.Marca = reader.GetString(2);
                    _autos.Modelo = reader.GetString(3);
                    _autos.Año = int.Parse(reader.GetString(4));
                    _autos.Color = reader.GetString(5);
                    _autos.Descripcion = reader.GetString(6);
                    _autos.Existencias = int.Parse(reader.GetString(7));
                    _autos.Precio = double.Parse(reader.GetString(8));
                    lista.Add(_autos);
                }
            }
            catch(MySqlException ex) // Informa si existe algún error en la conexión con la Base de Datos.
            {
                Console.WriteLine(ex.Message.ToString()); // Mensaje de consola en caso de error en la conexión.
            }
            return lista; // Devuelve el listado solicitado.
        }
    }
}
