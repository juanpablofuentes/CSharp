using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v3
{
    interface ILogica
    {
        int comprobar(string jugada1, string jugada2);
        string[] validas();
    }
}
