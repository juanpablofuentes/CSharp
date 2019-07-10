using System;
using System.Collections.Generic;

namespace first.Models
{
    public partial class Alumno
    {
        public int Idalumno { get; set; }
        public string Nombre { get; set; }
        public string Mail { get; set; }
        public int? Idcurso { get; set; }
        public Alumno(string nombre)
        {
            Nombre = nombre;
        }
        public virtual Curso Curso { get; set; }
    }
}
