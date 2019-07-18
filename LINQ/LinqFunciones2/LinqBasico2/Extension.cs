using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqBasico
{
    public static class Extension
    {
        public static bool esPrimo(this int i)
        {
            if (i <= 1) { return false; }
            if (i == 2) { return true; }
            for (int j = 2; j <= Math.Ceiling(Math.Sqrt(i)); j++)
            {
                if (i % j == 0) { return false; }
            }
            return true;
        }
    }
}
