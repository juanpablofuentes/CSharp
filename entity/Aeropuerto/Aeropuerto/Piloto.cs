using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Aeropuerto
{
   public  class Piloto
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal? Sueldo { get; set; }
        public List<Vuelo> Vuelos{ get; set; }
    }
}
