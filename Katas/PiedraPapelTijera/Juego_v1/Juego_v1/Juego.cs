using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v1
{
    class Juego
    {
        Jugador Jugador1 { get; set; }
        Jugador Jugador2 { get; set; }
        private string[] jugadas = { "piedra", "papel", "tijeras" };
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
        private string comprobar(Jugador jugador1, Jugador jugador2)
        {
         
            int pos1=Array.IndexOf(jugadas, jugador1.jugada.ToLower());
            int pos2 = Array.IndexOf(jugadas, jugador2.jugada.ToLower());
            if ((pos1 + 1) % 3 == pos2) return jugador2.Nombre;
            if ((pos2 + 1) % 3 == pos1) return jugador1.Nombre;
            return "Empate";
        }
    }
}
