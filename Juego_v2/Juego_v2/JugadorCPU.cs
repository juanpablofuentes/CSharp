using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v2
{
    class JugadorCPU : Jugador
    {
        public JugadorCPU(string nombre) : base(nombre)
        {
        }

        public override void pedirJugada(string[] validas)
        {
            Random random = new Random();
            this.jugada= validas[random.Next(0, validas.Length - 1)];
        }
    }
}
