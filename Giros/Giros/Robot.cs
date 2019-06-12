using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giros
{
    class Robot : IMovimientos,Automata,  ISensores
    {
        public string acelerar()
        {
            return "Acelerando";
        }

        public string bluetooth()
        {
            throw new NotImplementedException();
        }

        public string frenar()
        {
            return "Frenando";
        }

        public string girarDerecha()
        {
            return "Robot girando a la derecha";
        }

        public string girarIzquierda()
        {
            return "Robot girando a la izquierda";
        }

        public string infrarrojos()
        {
            throw new NotImplementedException();
        }

        public string saltar()
        {
            return "Pego un salto";
        }
    }
}
