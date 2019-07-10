using System;
using System.Collections.Generic;

namespace first.Models
{
    public partial class Profesor
    {
        public int IdProfesor { get; set; }
        public string Nombre { get; set; }
        public int? Idcurso { get; set; }

        public virtual Curso Curso { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
