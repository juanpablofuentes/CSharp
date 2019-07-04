using System;
using System.Collections.Generic;
using System.Text;

namespace EmpresaDespidos
{
    class Empresa
    {
        List<Empleado> plantilla = new List<Empleado>();
        public void contratar(Empleado emp)
        {
            plantilla.Add(emp);
        }
        public void despedir(Empleado emp)
        {
            if (plantilla.Contains(emp))
            {
                plantilla.Remove(emp);
                despido?.Invoke(emp);
            }
        }

        public delegate void accion(Empleado emp);
        public event accion despido;

    }
}
