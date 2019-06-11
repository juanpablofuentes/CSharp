using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v2
{
    class JuegoPPT : Juego
    {
        public JuegoPPT(Jugador jugador1, Jugador jugador2) : base(jugador1, jugador2)
        {
            this.jugadas=new string[]{ "piedra", "papel", "tijeras" };
        }

        public override string comprobar(Jugador jugador1, Jugador jugador2)
        {
            int pos1 = Array.IndexOf(jugadas, jugador1.jugada.ToLower());
            int pos2 = Array.IndexOf(jugadas, jugador2.jugada.ToLower());
            if ((pos1 + 1) % 3 == pos2) return jugador2.Nombre;
            if ((pos2 + 1) % 3 == pos1) return jugador1.Nombre;
            return "Empate";
        }
    }

}
