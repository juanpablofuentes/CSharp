using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Aeropuerto
{
   public class Avion
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public int Autonomia { get; set; }
    }
}
