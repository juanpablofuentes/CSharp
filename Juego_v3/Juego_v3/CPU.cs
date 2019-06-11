using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v3
{
    class CPU:IJugada
    {
        public string pedirJugada(string[] validas)
        {
            Random random = new Random();
            return validas[random.Next(0, validas.Length - 1)];
        }
    }
}
