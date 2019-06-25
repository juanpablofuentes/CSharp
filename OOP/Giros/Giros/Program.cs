using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giros
{
    class Program
    {
        static void Main(string[] args)
        {
            IGiros[] elementos = { new Vehiculo("ww"), new Torreta(), new Vehiculo("ee") };
            foreach(IGiros item in elementos)
            {
                Console.WriteLine(item.girarDerecha());
                Console.WriteLine(item.girarIzquierda());
            }
            Operario pepe = new Operario(new Robot());
            Operario eva = new Operario(new Vehiculo("Seat Panda"));

            pepe.trompo();
            eva.trompo();

            Robot walle = new Robot();
            walle.
        }
    }
}
