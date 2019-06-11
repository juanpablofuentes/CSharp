using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v3
{
    class Jugador
    {
        public string Nombre { get; set; }
        public string jugada { get; set; }
        public Jugador(string nombre, IJugada ijugada)
        {
            this.Nombre = nombre;
            this._ijugada = ijugada;
        }
        private IJugada _ijugada;
        public void pedirJugada(string[] validas)
        {
            this.jugada = _ijugada.pedirJugada(validas);
        }
    }
}