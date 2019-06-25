using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giros
{
    interface IGiros
    {
        string girarDerecha();
        string girarIzquierda();
    }
    interface IVelocidad : IGiros
    {
        string acelerar();
        string frenar();
    }
    interface IMovimientos : IVelocidad
    {
        string saltar();
    }
}
