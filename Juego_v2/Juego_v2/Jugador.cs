using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v2
{
    abstract class Jugador
    {
        public string Nombre { get; set; }
        public string jugada { get; set; }
        public Jugador(string nombre)
        {
            this.Nombre = nombre;
        }
        public abstract void pedirJugada(string[] validas);
      
    }
}
