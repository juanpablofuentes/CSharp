using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace MVCComics
{
    public class Comic
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public ICollection<ComicAutor> ComicAutor { get; set; }
    }
}