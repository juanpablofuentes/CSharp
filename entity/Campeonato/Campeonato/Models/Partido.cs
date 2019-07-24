using System;
using System.Collections.Generic;

namespace Campeonato.Models
{
    public partial class Partido
    {
        public int Id { get; set; }
        public int? Idjugador { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual Jugador IdjugadorNavigation { get; set; }
    }
}
