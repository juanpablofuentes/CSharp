using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWeb2.Models
{
    public class Game
    {
        public int Id { get; set; }
        public String Titulo { get; set; }
        public Double Precio { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
