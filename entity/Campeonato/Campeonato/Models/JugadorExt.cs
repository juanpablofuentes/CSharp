using System;
using System.Collections.Generic;
using System.Text;

namespace Campeonato.Models
{
    public partial class Jugador
    {
        public override string ToString()
        {
            return Nombre + Puntos;
        }
    }
}
