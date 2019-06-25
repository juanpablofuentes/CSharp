using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea
{
    public class Armario : IHerramientas, IPartes
    {
        public string[] GetHerramientas()
        {
            return new string[] { "Destornillador","Sierra", "Llave Allen" };
        }

        public string[] GetPartes()
        {
            return new string[] {
            "lateral", "lateral", "fondo", "puerta", "piso","techo", "tornillos GT5" };
        }
    }
}
