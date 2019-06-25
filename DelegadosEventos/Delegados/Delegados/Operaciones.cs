using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegados
{
    class Operaciones
    {
        public static double doble(double num)
        {
            return num * 2;
        }
        public static double mitad(double num)
        {
            return num / 2;
        }
        public static double cuadrado(double num)
        {
            return num *num;
        }
        public static double raiz(double num)
        {
            return Math.Sqrt(num);
        }
    }
}
