using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas
{
    class Program
    {
        static void Main(string[] args)
        {
            EstadoVentas v = new EstadoVentas();

            v.agregarVenta("Ana", 10);
            v.agregarVenta("Eva", 20);
            v.agregarVenta("Rosa", 40);
            Console.WriteLine(v);
            v.agregarVenta("Rosa", 40);
            Console.WriteLine(v);
            Console.WriteLine(v.totalVentas());
            Console.WriteLine(v.mediaVentas());
            Console.WriteLine(v.mejorVendedor());
            Console.WriteLine(v.mejorVendedorLinq());
        }
    }
}
