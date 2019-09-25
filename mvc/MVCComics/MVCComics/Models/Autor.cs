using System;
using System.Collections.Generic;
using System.Text;

namespace MVCComics
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Nacionalidad { get; set; }
        public int AnyNacimiento { get; set; }
        public ICollection<ComicAutor> ComicAutor { get; set; }
    }
}
