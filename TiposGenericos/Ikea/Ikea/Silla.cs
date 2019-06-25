using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea
{
    public class Silla : IHerramientas, IPartes
    {
        public string[] GetHerramientas()
        {
            return new string[] { "Destornillador", "Llave Allen" };
        }

        public string[] GetPartes()
        {
            return new string[] {
            "pata", "pata", "pata", "asiento", "respaldo", "tornillos GT5" };
        }
    }
}
