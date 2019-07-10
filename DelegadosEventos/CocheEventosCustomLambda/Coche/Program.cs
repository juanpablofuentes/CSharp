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
            c1.AlLimite += (object sender, ArgumentoEventosCoche e) =>
        { Console.WriteLine(e.Mensaje); };
            c1.AlLimite += (object sender, ArgumentoEventosCoche e) =>
        { Console.WriteLine("=> Mensaje crítico: {0}", e.Mensaje); };
            c1.Explotar += (object sender, ArgumentoEventosCoche e) =>
        { Console.WriteLine("BOOOM" + e.Mensaje); };


            // Acelerar
            Console.WriteLine("***** acelerando *****");
            for (int i = 0; i < 6; i++)
                c1.Acelerar(20);


            c1.Velocidad = 0;
            c1.restaurar();
            // Ya no salen mayúsculas
            Console.WriteLine("***** Acelerando *****");
            for (int i = 0; i < 12; i++)
                c1.Acelerar(10);

            Console.ReadLine();
        }

    }
}
