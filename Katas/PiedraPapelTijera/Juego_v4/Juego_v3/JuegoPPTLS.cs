using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v3
{
    class JuegoPPTLS : ILogica
    {
        private string[] jugadas = { "piedra", "papel", "tijeras", "spock", "lagarto" };

        public int comprobar(string jugada1, string jugada2)
        {
            int res = 0;
            int pos1 = Array.IndexOf(jugadas, jugada1.ToLower());
            int pos2 = Array.IndexOf(jugadas, jugada2.ToLower());
            if ((pos1 + 1) % this.jugadas.Length == pos2 || (pos1 + 3) % this.jugadas.Length == pos2) res= 2;
            if ((pos2 + 1) % this.jugadas.Length == pos1 || (pos2 + 3) % this.jugadas.Length == pos1) res= 1;
            
            return res;
        }

        string[] ILogica.validas()
        {
            return this.jugadas;
        }
    }
    
    
}
