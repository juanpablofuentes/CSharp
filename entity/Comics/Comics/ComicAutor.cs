﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Comics
{
    public class ComicAutor
    {
        public int Id { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
        public int ComicId { get; set; }
        public Comic Comic { get; set; }
    }
}
