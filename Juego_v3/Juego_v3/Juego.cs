using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v3
{
    class Juego
    {
        Jugador Jugador1 { get; set; }
        Jugador Jugador2 { get; set; }
        ILogica _ilogica;
        public Juego(Jugador jugador1, Jugador jugador2, ILogica ilogica)
        {
            this.Jugador1 = jugador1;
            this.Jugador2 = jugador2;
            this._ilogica = ilogica;
        }
        public string jugar()
        {
            this.Jugador1.pedirJugada(_ilogica.validas());
            this.Jugador2.pedirJugada(_ilogica.validas());
            int res= _ilogica.comprobar(Jugador1.jugada, Jugador2.jugada);
            if (res == 1) { return Jugador1.Nombre; }
            if (res == 2) { return Jugador2.Nombre; }
            return "Empate"; 
        }
    }
}
