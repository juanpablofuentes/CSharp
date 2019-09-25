using System;
using System.Collections.Generic;
using System.Text;

namespace MVCComics
{
    public class ComicAutor
    {
        public int Id { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
        public int ComicId { get; set; }
        public Comic Comic { get; set; }
        public string Rol { get; set; }
    }
}
