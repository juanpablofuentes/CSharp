using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Jardin
{
   public  class Jardinero
    {
        [Key]
        public int Clave { get; set; }
        [Required]
        [StringLength (100)]
        public string Nombre { get; set; }
        [Range(1000,2000)]
        public decimal? Sueldo { get; set; }
    }
}
