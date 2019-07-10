using System;
using System.Collections.Generic;

namespace first.Models
{
    public partial class Curso
    {
        public Curso()
        {
            Alumno = new HashSet<Alumno>();
            Profesor = new HashSet<Profesor>();
        }

        public int Idcurso { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Alumno> Alumno { get; set; }
        public virtual ICollection<Profesor> Profesor { get; set; }
    }
}
