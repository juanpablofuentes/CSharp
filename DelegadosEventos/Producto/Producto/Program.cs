using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto
{
    class Program
    {

        static void Main(string[] args)
        {
            Producto p = new Producto("Chupa de cuero", 100);
            cambiar c = descuento;
            c += aumento;
            c += rebaja;
            p.cambiarPrecio(c);
            Console.WriteLine(p);

        }
        public static double descuento(double valor)
        {
            Console.WriteLine(valor);
            return valor*.9;

        }
        public static double aumento(double valor)
        {
            Console.WriteLine(valor);
            return valor*1.05;
        }
        public static double rebaja(double valor)
        {
            Console.WriteLine(valor);
            return valor/2;
        }
    }
}
