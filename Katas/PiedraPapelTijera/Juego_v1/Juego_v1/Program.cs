using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v1
{
    class Program
    {
        static void Main(string[] args)
        {
            Jugador eva = new Jugador("Eva");
            Jugador juan = new Jugador("Juan");
            Juego game = new Juego(eva, juan);
            Console.WriteLine("Ganador: "+game.jugar());
        }
    }
}
