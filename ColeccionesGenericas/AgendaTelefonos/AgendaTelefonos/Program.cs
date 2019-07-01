using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaTelefonos
{
    class Program
    {
        static void Main(string[] args)
        {
            Agenda a = new Agenda();
            a.agregarContacto("Ana", "22");
            a.agregarContacto("pepe", "22");
            a.agregarContacto("Ana", "3333");
            a.agregarContacto("Eva", "3333");
            a.agregarContacto("Juan", "3333");
            a.agregarContacto("Rosa", "3333");
            Console.WriteLine(a);
            Console.WriteLine(   String.Join(",",a.repetidos()));
        }
    }
}
