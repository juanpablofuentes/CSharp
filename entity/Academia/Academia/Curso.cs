using System;
using System.Collections.Generic;
using System.Text;

namespace Academia
{
    class Curso
    {
        public int Id { get; set; } 
        public string Nombre { get; set; }
        public ICollection<Matricula> Matriculas{ get; set; }
    }
}
