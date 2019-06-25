using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v2
{
    class Program
    {
        static void Main(string[] args)
        {
            Jugador eva = new JugadorHumano("Eva");
            Jugador juan = new JugadorHumano("Juan");
            Juego game = new JuegoPPTLS(eva, juan);
            for(int i = 0; i < 20; i++) { 
            Console.WriteLine("Ganador: " + game.jugar());
            }
        }
    }
}
