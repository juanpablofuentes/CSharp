using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato
{
    class Equipo
    {
        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public Equipo(string nombre)
        {
            Nombre = nombre;
        }
        public override bool Equals(object obj)
        {
            Equipo p = (Equipo)obj;
            return Nombre.Equals(p.Nombre);
        }
        public override string ToString()
        {
            return Nombre;
        }

    }
}
