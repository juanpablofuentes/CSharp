using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v3
{
    class Colores : ILogica
    {
        private string[] _validas = { "Rojo", "Verde", "Azul" };
        public Colores()
        {

        }
        public int comprobar(string jugada1, string jugada2)
        {
            if (jugada1 == "Azul") return 1;
            if (jugada2 == "Azul") return 2;
            return 0;
        }

        public string[] validas()
        {
            return this._validas;
        }
    }
}
