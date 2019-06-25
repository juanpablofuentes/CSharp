using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v2
{
    abstract class Juego
    {
        Jugador Jugador1 { get; set; }
        Jugador Jugador2 { get; set; }
        protected string[] jugadas;
        public Juego(Jugador jugador1, Jugador jugador2)
        {
            this.Jugador1 = jugador1;
            this.Jugador2 = jugador2;
        }
        public string jugar()
        {
            this.Jugador1.pedirJugada(this.jugadas);
            this.Jugador2.pedirJugada(this.jugadas);
            return comprobar(Jugador1, Jugador2);
        }
        public abstract  string comprobar(Jugador jugador1, Jugador jugador2);
    }
}
