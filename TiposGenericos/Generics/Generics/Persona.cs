using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    class Persona
    {
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public Persona(string nombre)
        {
            Nombre = nombre;
        }
        public override bool Equals(Object obj)
        {
            return Nombre==((Persona) obj).Nombre;
        }
    }
}
