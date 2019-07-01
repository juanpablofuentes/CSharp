using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    class Tanque : IGiros
    {
        private int posicion;

        public int Posicion
        {
            get { return posicion; }
            set { posicion = value; }
        }

        public void girarDerecha()
        {
            Posicion += 15;
            Posicion %= 360;
        }

        public void girarIzquierda()
        {
            Posicion -= 15;
            Posicion += 360;
            Posicion %= 360;
        }
    }
}
