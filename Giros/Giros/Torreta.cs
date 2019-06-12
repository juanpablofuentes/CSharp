using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giros
{
    class Torreta : IGiros
    {
        public string girarDerecha()
        {
            return "Girando una torreta a la derecha";
        }

        public string girarIzquierda()
        {
            return "Girando una torreta a la izquierda";
        }
    }
}
