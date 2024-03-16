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
            MySqlDataReader reader;
            List<Object> lista = new List<Object>();
            string sql;

            if(dato == null)
            {
                sql = "SELECT Id, Codigo, Marca, Modelo, Año, Color, Descripcion, Existencias, Precio FROM autos ORDER BY Marca ASC";
            }
            else
            {
                sql = "SELECT Id, Codigo, Marca, Modelo, Año, Color, Descripcion, Existencias, Precio FROM autos WHERE Codigo LIKE '%" + dato + "%' OR Marca LIKE '%" + dato + "%' OR Modelo LIKE '%" + dato + "%' OR Año LIKE '%" + dato + "%' OR Color LIKE '%" + dato + "%' OR Descripcion LIKE '%" + dato + "%' OR Existencias LIKE '%" + dato + "%' OR Precio LIKE '%" + dato + "%' ORDER BY Marca ASC";
            }

            try
            {
                MySqlConnection conexionBD = Conexion.conexion();
                conexionBD.Open();
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();

                while (reader.Read())
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
            catch(MySqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return lista;
        }
    }
}
