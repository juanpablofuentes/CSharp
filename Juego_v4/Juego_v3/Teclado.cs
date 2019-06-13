using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v3
{
    class Teclado:IJugada
    {
        public string pedirJugada(string[] validas)
        {
            string jugada;
            do
            {
                Console.WriteLine( "Introduzca jugada");
                jugada = Console.ReadLine();
            } while (!validas.Contains(jugada));
            return jugada;
        }
    }
}
