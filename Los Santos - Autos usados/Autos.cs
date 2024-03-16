using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Los_Santos___Autos_usados
{
    internal class Autos
    {
        // Se declara el tipo de dato permitido en cada campo del formulario.
        private int id;
        private string codigo;
        private string marca;
        private string modelo;
        private int año;
        private string color;
        private string descripcion;
        private int existencias;
        private double precio;

        // Se declaran los datos de los campos del formulario para la conexión a la Base de Datos.
        public int Id { get => id; set => id = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Marca { get => marca; set => marca = value; }
        public string Modelo { get => modelo; set => modelo = value; }
        public int Año { get => año; set => año = value; }
        public string Color { get => color; set => color = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Existencias { get => existencias; set => existencias = value; }
        public double Precio { get => precio; set => precio = value; }
    }
}
