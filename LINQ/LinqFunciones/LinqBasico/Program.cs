using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqBasico
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Alumno> listaAlumnos = new List<Alumno>()
            {
                new Alumno("Eva",20,6.0),
                new Alumno("Ana",22,7.0),
                new Alumno("Rosa",22,4.0),
                new Alumno("Ot",20,3.0),
                new Alumno("Iu",30,6.8),
                new Alumno("Pep",32,5.9),
                new Alumno("Laia",30,2.3),
                new Alumno("Quim",32,1.7),
            };

            //Elemento en una posición
            Console.WriteLine(listaAlumnos.ElementAt(0));
            //Elemento en una posición si está en el rango, si no el default
            Console.WriteLine(listaAlumnos.ElementAtOrDefault(0));
            // Console.WriteLine(listaAlumnos.ElementAt(10));
            Console.WriteLine(listaAlumnos.ElementAtOrDefault(10));

            Console.WriteLine(listaAlumnos.First());
            Console.WriteLine(listaAlumnos.FirstOrDefault());

            Console.WriteLine(listaAlumnos.Last());
            Console.WriteLine(listaAlumnos.LastOrDefault());

            List<Alumno> listaAlumnos2 = new List<Alumno>();

            //Este da un error
            //Console.WriteLine(listaAlumnos2.First());
            Console.WriteLine(listaAlumnos2.FirstOrDefault());

            Console.WriteLine(listaAlumnos.Single(el=>el.Nota>=7));
            //Error porque hay más de uno
            //Console.WriteLine(listaAlumnos.Single(el=>el.Edad>30));
            //Console.WriteLine(listaAlumnos.SingleOrDefault(el=>el.Edad>30));
             listaAlumnos2 = new List<Alumno>(){
                new Alumno("Eva",20,6.0),
                new Alumno("Ana",22,7.0),
                new Alumno("Rosa",22,4.0),
                new Alumno("Ot",20,3.0),
                new Alumno("Iu",30,6.8),
                new Alumno("Pep",32,5.9),
                new Alumno("Laia",30,2.3),
                new Alumno("Quim",32,1.7),
            };

            //La misma secuencia en el mismo orden
            Console.WriteLine(listaAlumnos.SequenceEqual(listaAlumnos2));

          //  Concatenar dos listas
            IEnumerable<Alumno> lista3 = listaAlumnos.Concat(listaAlumnos2);

            Console.WriteLine(String.Join(",",lista3));
            Console.WriteLine("--");
            Console.WriteLine(String.Join(",",lista3.Distinct()));
            Console.WriteLine("--");
            Console.WriteLine(String.Join(",",lista3.Except(listaAlumnos)));
            Console.WriteLine("--");
            Console.WriteLine(String.Join(",",lista3.Intersect(listaAlumnos)));
            Console.WriteLine("--");
            Console.WriteLine(String.Join(",",lista3.Union(listaAlumnos)));
            Console.WriteLine("--");
            Console.WriteLine(String.Join(",", lista3.Skip(2)));
            Console.WriteLine("--");
            Console.WriteLine(String.Join(",", lista3.SkipWhile(el=>el.Edad<30)));
        }


    }
}
