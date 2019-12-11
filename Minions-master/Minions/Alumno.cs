using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minions
{
    class Alumno
    {
        public Alumno(string nombre)
        {
            this.nombre = nombre;
        }
        public string nombre { get; set; }
        public int salidas { get; set; }

        public bool valido
        {
            get { return salidas >= 0; }
        }

        public override string ToString()
        {
            return this.nombre;
        }
    }
}
