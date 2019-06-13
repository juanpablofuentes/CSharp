using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v3
{
    class Program
    {
        static void Main(string[] args)
        {
            Jugador eva = new Jugador("Eva", new Teclado());
            Jugador juan = new Jugador("Juan", new CPU());
            Juego ppt = new Juego(eva, juan, new JuegoPPTLS());
            ppt.jugar();
        }
    }
}
