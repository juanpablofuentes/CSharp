using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayGenerics
{
    class Animal
    {
        public string Nombre  { get; set; }
        public string Sonido { get; set; }
        public Animal(string nombre)
        {
            Nombre = nombre;
        }
        public override string ToString()
        {
            return Nombre;
        }
      
    }
    class Planta
    {
        public string Clasificacion { get; set; }
        public Planta(string clasificacion)
        {
            Clasificacion = clasificacion;
        }
        public override string ToString()
        {
            return Clasificacion;
        }

    }
    class Mineral
    {
        public string Denominacion { get; set; }
        public Mineral(string denominacion)
        {
            Denominacion = denominacion;
        }
        public override string ToString()
        {
            return Denominacion;
        }
    }
}
