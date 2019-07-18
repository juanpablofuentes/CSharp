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
            List<int> enteros = Enumerable.Range(1, 20).ToList();

            var ent = enteros.Where(el => el % 2==0);
            Console.WriteLine(String.Join(",",ent));
            Console.WriteLine(   enteros.Where(el => el.esPrimo()).Sum());
            Console.WriteLine(enteros.Aggregate<int>(
                (a, b) => a + b
                
                )); ;
            Console.WriteLine(String.Join(",", ent));
         
            //Seleccionamos una condición
            var res = listaAlumnos.Where(aprobado);
            Console.WriteLine(String.Join(", ",res));
            res = listaAlumnos.Where(s => s.Nota >= 5);
            Console.WriteLine(String.Join(", ", res));
            //Seleccionamos un índice
            res = listaAlumnos.Where((s,index) => index%2==0);
            Console.WriteLine(String.Join(", ", res));
            //Condición compleja
            res = listaAlumnos.Where(s => s.Nota >= 5&&s.Nota<7);
            Console.WriteLine(String.Join(", ", res));


            //Ordenar
            res = listaAlumnos.Where(s => s.Nota >= 5).OrderBy(s => s.Edad);
            Console.WriteLine(String.Join(", ", res));
            res = listaAlumnos.Where(s => s.Nota >= 5).OrderBy(s => s.Edad).ThenByDescending(s=>s.Nota);
            Console.WriteLine(String.Join(", ", res));
            var agrupado = listaAlumnos.GroupBy(s => s.Edad);
            foreach (var grupo in agrupado)
            {
                Console.WriteLine("Agrupado por el valor: " + grupo.Key);
                foreach (Alumno al in grupo)
                {
                    Console.WriteLine(al);
                }
            }

            //Comprobar
            Console.WriteLine(listaAlumnos.All(s=>s.Nota>5));
            Console.WriteLine(listaAlumnos.All(s => s.Nota > 1));
            Console.WriteLine(listaAlumnos.Any(s => s.Nota > 5));

            //Funciones de agregado
            Console.WriteLine(listaAlumnos.Max(s=>s.Nota));
            Console.WriteLine(listaAlumnos.Min(s => s.Nota));
            Console.WriteLine(listaAlumnos.Sum(s => s.Nota));
            Console.WriteLine(listaAlumnos.Average(s => s.Nota));
            Console.WriteLine(listaAlumnos.Aggregate<Alumno,string>(
                                        "Alumnos: ",  // seed value
                                        (str, s) => str += s.Nombre + ","));
        }


        static bool aprobado(Alumno al)
        {
            return al.Nota >= 5;
        }
    }
}
