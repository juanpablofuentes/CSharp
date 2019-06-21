using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato
{
    class Partido
    {
        public Equipo Local { get; set; }
        public Equipo Visitante { get; set; }
        public Partido(Equipo local, Equipo visitante)
        {
            Local = local;
            Visitante = visitante;
        }
        public override string ToString()
        {
            return Local+" - " + Visitante;
        }
    }
}
