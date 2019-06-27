using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Herencia
{
    class Funcionario : Persona
    {
        private Queue<Persona> fila;
        public Funcionario(string nombre) : base(nombre)
        {
            fila = new Queue<Persona>();
        }
        public void turno(Persona p)
        {
            if (fila.Contains(p))
            {
                throw new Exception("Ya en la fila");
            }
            fila.Enqueue(p);
            Console.WriteLine("Añadido a la fila "+p.Nombre);
        }
        public void atender() {
            Persona p = fila.Dequeue();
            p.saludo();
            Console.WriteLine("Atendido "+p.Nombre);
        }

    }
}
