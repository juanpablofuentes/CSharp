using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqBasico
{
    class Alumno
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public double Nota { get; set; }
        public Alumno(string nombre, int edad, double nota)
        {
            Nombre = nombre;
            Edad = edad;
            Nota = nota;
        }
        public override string ToString()
        {
            return Nombre+" - "+Edad+" - "+Nota;
        }
    }
}
