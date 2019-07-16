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
            //  Seleccionamos todos los alumnos (devuelve un IEnumerable de  tipo Alumno
            var res = from alumno in listaAlumnos select alumno;
            foreach (Alumno al in res)
            {
                Console.WriteLine(al);
            }
            Console.WriteLine(String.Join(",", res));
            //  Seleccionamos sólo el nombre de los alumnos (devuelve un IEnumerable de tipo string
            var res2 = from alumno in listaAlumnos select alumno.Nombre;
            foreach (string al in res2)
            {
                Console.WriteLine(al);
            }
            Console.WriteLine(String.Join(",", res2));
            //Sólo los aprobados
            res = from alumno in listaAlumnos where alumno.Nota >= 5 select alumno;

            Console.WriteLine(String.Join(" | ", res));
            listaAlumnos.Add(new Alumno("Einstein", 50, 10));
            //Vuelve a ejecutarse la consulta LINQ
            Console.WriteLine(String.Join(" | ", res));

            //Ejecución inmediata
            List<Alumno> inmediato = (from alumno in listaAlumnos where alumno.Nota >= 5 select alumno).ToList<Alumno>();
            //Podemos usar las funciones de los enumerables
            Console.WriteLine(res.Count());

            Console.WriteLine(String.Join(" | ", res.Reverse()));
            //Ordenadr ascendente o descendente
            res = from alumno in listaAlumnos where alumno.Nota >= 5 orderby alumno.Nota descending select alumno;
            Console.WriteLine(String.Join(" | ", res));


            //Agrupando
            var agrupado = from alumno in listaAlumnos group alumno by alumno.Nombre.Length;
            Console.WriteLine(  String.Join(",",agrupado.First())); 
            Console.WriteLine(agrupado.Count());
            foreach (var grupo in agrupado)
            {
                Console.WriteLine("Agrupado por el valor: " + grupo.Key + " valores " + grupo.Count());
                foreach (Alumno al in grupo)
                {
                    Console.WriteLine(al);
                }
            }

            agrupado = from alumno in listaAlumnos group alumno by alumno.Edad into g orderby g.Key descending select g;
            foreach (var grupo in agrupado)
            {
                Console.WriteLine("Agrupado por el valor: " + grupo.Key);
                foreach (Alumno al in grupo)
                {
                    Console.WriteLine(al);
                }
            }
            Console.WriteLine("-----");
            agrupado = from alumno in listaAlumnos group alumno by alumno.Nombre.Length into g where g.Sum(al=>al.Nota) > 10 orderby g.Key descending select g;
            foreach (var grupo in agrupado)
            {
                Console.WriteLine("Agrupado por el valor: " + grupo.Key);
                Console.WriteLine(String.Join(",", grupo));
            }
        }
    }
}
