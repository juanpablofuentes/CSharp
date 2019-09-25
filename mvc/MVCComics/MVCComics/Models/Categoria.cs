using System;
using System.Collections.Generic;
using System.Text;

namespace MVCComics
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Comic> Comics { get; set; }
    }
}
