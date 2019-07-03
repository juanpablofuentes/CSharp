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
            c1.RegistrarManejador(EventoCoche);

            // Registramos otro
            Coche.ManejarMotor handler2 = new Coche.ManejarMotor(EventoCoche2);
            c1.RegistrarManejador(handler2);

            // Acelerar
            Console.WriteLine("***** acelerando *****");
            for (int i = 0; i < 6; i++)
                c1.Acelerar(20);

            // Quitar el segundo manejador
            c1.QuitarManejador(handler2);
            c1.Velocidad = 0;
            c1.restaurar();
            // Ya no salen mayúsculas
            Console.WriteLine("***** Acelerando *****");
            for (int i = 0; i < 10; i++)
                c1.Acelerar(10);

            Console.ReadLine();
        }
        public static void EventoCoche(string msg)
        {
            Console.WriteLine("\n***** Mensaje del objeto *****");
            Console.WriteLine("=> {0}", msg);
            Console.WriteLine("***********************************\n");
        }

        public static void EventoCoche2(string msg)
        {
            Console.WriteLine("=> {0}", msg.ToUpper());
        }
    }
}
