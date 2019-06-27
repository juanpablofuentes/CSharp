using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareas
{
    class Program
    {
        static void Main(string[] args)
        {
            Programa p = new Programa();
            p.add(new Tarea("Modelo ER", "Eva", 5));
            p.add(new Tarea("Interfaz", "Juan", 15));
            p.add(new Tarea("HTML", "Ana", 2));
            p.add(new Tarea("Servidor", "Rosa", 6));
            p.add(new Tarea("Node", "Pep", 8));
            Console.WriteLine(p);
            Console.WriteLine(p.tiempo());
            p.optimizar();
            Console.WriteLine(p);
            Console.WriteLine(p.procesar());
            Console.WriteLine(p);
            foreach(Tarea t in p)
            {
                Console.WriteLine(t.Nombre);
            }


        }
    }
}
