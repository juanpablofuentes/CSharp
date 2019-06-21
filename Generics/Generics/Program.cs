using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            ClaseGenerica<int> claseEntero = new ClaseGenerica<int>(10);

            int val = claseEntero.metodoGenerico(200);

            ClaseGenerica<string> strGenericClass = new ClaseGenerica<string>("Hola ¿Qué tal?");

            strGenericClass.propiedadGenerica = "Ejemplo de propiedad genérica";
            string result = strGenericClass.metodoGenerico("Parámetro genérico");
        }
    }
}
