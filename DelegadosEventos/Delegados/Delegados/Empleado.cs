using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegados
{
    class Empleado
    {
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public Empleado(string nombre)
        {
            Nombre = nombre;
        }
        public void ejecutar(Empresa accion)
        {
            accion(nombre);
        }
    }
}
