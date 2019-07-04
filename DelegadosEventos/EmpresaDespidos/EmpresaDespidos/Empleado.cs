using System;
using System.Collections.Generic;
using System.Text;

namespace EmpresaDespidos
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
    }
}
