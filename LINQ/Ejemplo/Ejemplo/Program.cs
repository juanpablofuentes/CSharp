using System;
using System.Linq;
using System.Collections.Generic;

namespace Ejemplo
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<int> lista = new List<int> { 10, 11, 12, 21, 22, 24, 30, 34, 36 };

            var agrupado = from l in lista group l by l % 10 into g where g.Sum() >= 40 select g;

            foreach (var grupo in agrupado)
            {

                Console.WriteLine(grupo.Key);
                Console.WriteLine(String.Join(",", grupo));

            }

        }
    }
}
