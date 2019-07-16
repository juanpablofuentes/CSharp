using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionMetodo
{
    public static class Extension
    {
        public static bool mayorQue(this int i, int value)
        {
            return i > value;
        }
        public static bool esPar(this int i)
        {
            return i % 2 == 0;
        }
        public static bool esPrimo(this int i)
        {
            if (i<=1) { return false; }
            if (i==2) { return true; }
            for(int j=2; j<= Math.Ceiling(Math.Sqrt(i)); j++)
            {
                if (i%j==0) { return false; }
            }
            return true;
        }
        public static int siguientePrimo(this int i)
        {
            do { i++; }
            while (!i.esPrimo()) ;
            return i;
        }
    }
}
