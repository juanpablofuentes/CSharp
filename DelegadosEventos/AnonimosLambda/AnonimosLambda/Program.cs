using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonimosLambda
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>() { 20, 1, 4, 8, 9, 44 };

            List<int> pares = list.FindAll(esPar);
            Console.WriteLine(String.Join(",",pares));
            pares = list.FindAll(delegate(int n) { return n % 2 == 0; });
            Console.WriteLine(String.Join(",", pares));
            pares = list.FindAll(n=>n%2==0);
            Console.WriteLine(String.Join(",", pares));
            pares= list.FindAll(i =>
            {
                Console.WriteLine("Valor de i: {0}", i);
                bool esPar = ((i % 2) == 0);
                return esPar;
            });
            Console.WriteLine(String.Join(",", pares));
        }
        static bool esPar(int n)
        {
            return n % 2 == 0;
        }
    }
}
