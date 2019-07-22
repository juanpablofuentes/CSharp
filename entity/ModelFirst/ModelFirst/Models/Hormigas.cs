using System;
using System.Collections.Generic;

namespace ModelFirst.Models
{
    public partial class Hormigas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string JuegoFavorito { get; set; }
    }
}
