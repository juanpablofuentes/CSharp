using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v2
{
    class JugadorHumano : Jugador
    {
        public JugadorHumano(string nombre) : base(nombre)
        {
        }

        public override void pedirJugada(string[] validas)
        {
            do
            {
                Console.WriteLine(this.Nombre + " introduzca jugada");
                this.jugada = Console.ReadLine();
            } while (!validas.Contains(this.jugada));
        }
    }
}
