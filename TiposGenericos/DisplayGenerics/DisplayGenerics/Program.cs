using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayGenerics
{
    class Program
    {
        static void Main(string[] args)
        {
            Coleccion<Animal> zoo = new Coleccion<Animal>();

            zoo.add(new Animal("Leon"));
            zoo.add(new Animal("Cabra"));
            zoo.add(new Animal("Cerdo"));
            zoo.mostrar();

            Coleccion<Planta> jardin = new Coleccion<Planta>();

            jardin.add(new Planta("Ficus"));
            jardin.add(new Planta("Abeto"));
            jardin.add(new Planta("Margarita"));
            jardin.mostrar();
        }
    }
}
