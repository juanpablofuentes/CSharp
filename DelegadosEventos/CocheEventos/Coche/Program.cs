using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coche
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crear objeto
            Coche c1 = new Coche("Panda", 100, 10);
            c1.AlLimite+=CocheAlLimite;
            c1.AlLimite += CocheCrítico;
            c1.Explotar += CocheExplotado;

        
            // Acelerar
            Console.WriteLine("***** acelerando *****");
            for (int i = 0; i < 6; i++)
                c1.Acelerar(20);

            // Quitar el segundo manejador
            c1.AlLimite -= CocheCrítico;
            c1.Velocidad = 0;
            c1.restaurar();
            // Ya no salen mayúsculas
            Console.WriteLine("***** Acelerando *****");
            for (int i = 0; i < 12; i++)
                if (!c1.Muerto)
                c1.Acelerar(10);

            Console.ReadLine();
        }
        public static void CocheAlLimite(string msg)
        { Console.WriteLine(msg); }

        public static void CocheCrítico(string msg)
        { Console.WriteLine("=> Mensaje crítico: {0}", msg); }

        public static void CocheExplotado(string msg)
        { Console.WriteLine(msg); }
    }
}
