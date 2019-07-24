using System;
using System.Collections.Generic;

namespace Campeonato.Models
{
    public partial class Jugador
    {
        public Jugador()
        {
            Partido = new HashSet<Partido>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? Puntos { get; set; }
        public string Alias { get; set; }

        public virtual ICollection<Partido> Partido { get; set; }
        
    }
}
