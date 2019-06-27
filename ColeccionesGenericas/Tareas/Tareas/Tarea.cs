using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareas
{
    class Tarea
    {
        public string Nombre { get; set; }
        public string Empleado { get; set; }
        public int Horas { get; set; }
        public Tarea(string nombre,string empleado, int horas)
        {
            Nombre = nombre;
            Empleado = empleado;
            Horas = horas;
        }
        public override string ToString()
        {
            return Nombre+ " "+Empleado+" "+Horas;
        }
    }
}
