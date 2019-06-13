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
        IMostrar _imostrar;
        public Juego(Jugador jugador1, Jugador jugador2, ILogica ilogica, IMostrar imostrar):this(jugador1,jugador2,ilogica)
        {
            this._imostrar = imostrar;
        }
        public Juego(Jugador jugador1, Jugador jugador2, ILogica ilogica)
        {
            this.Jugador1 = jugador1;
            this.Jugador2 = jugador2;
            this._ilogica = ilogica;
            this._imostrar = new Consola();
        }
        public string jugar()
        {
            this.Jugador1.pedirJugada(_ilogica.validas());
            this.Jugador2.pedirJugada(_ilogica.validas());
            int res= _ilogica.comprobar(Jugador1.jugada, Jugador2.jugada);
            _imostrar.mostrar(Jugador1, Jugador2, res);
            if (res == 1) { return Jugador1.Nombre; }
            if (res == 2) { return Jugador2.Nombre; }
            return "Empate"; 
        }
    }
}
