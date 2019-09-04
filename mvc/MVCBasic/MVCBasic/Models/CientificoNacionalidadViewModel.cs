using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasic.Models
{
    public class CientificoNacionalidadViewModel
    {
       
            public List<Cientifico> Cientificos { get; set; }
            public SelectList Nacionalidades { get; set; }
            public string CientificoNacionalidad { get; set; }
            public string Buscar { get; set; }
        
    }
}
