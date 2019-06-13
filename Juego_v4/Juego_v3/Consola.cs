using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v3
{
    class Consola : IMostrar
    {
        public void mostrar(Jugador jug1, Jugador jug2, int res)
        {
            string ganador = "empate";
            Console.WriteLine(jug1.Nombre+" - "+jug1.jugada);
            Console.WriteLine(jug2.Nombre + " - " + jug2.jugada);
            if (res == 1) { ganador = jug1.Nombre; }
            if (res == 2) { ganador = jug2.Nombre; }
            Console.WriteLine(ganador);
           
        }
    }
}
