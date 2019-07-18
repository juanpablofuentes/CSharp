using System;
using System.Collections.Generic;
namespace ExtensionMetodo
{
    class Program
    {
        static void Main(string[] args)
        {

            int i= 214;
            string cadena = "";

          
            Console.WriteLine(i.mayorQue(5));
            Console.WriteLine(i.mayorQue(9));
            Console.WriteLine(i.esPar());
            Console.WriteLine(Extension.par(i));
            Console.WriteLine(i.esPrimo());
            List<int> l = i.factores();
            Console.WriteLine(String.Join(",",l));

            Console.WriteLine(i.siguientePrimo());

            string cad = "supercalifragilisticoespialidoso";
            Console.WriteLine(String.Join(",",cad.anagrama()));
            
        }
    }
}
