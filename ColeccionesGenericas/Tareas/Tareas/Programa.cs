using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareas
{
    class Programa:IEnumerable<Tarea>
    {
        private Queue<Tarea> Lista = new Queue<Tarea>();
        public void add(Tarea t)
        {
            Lista.Enqueue(t);
        }
        public string procesar()
        {
            Tarea t = Lista.Dequeue();
            return t.ToString();
        }
        public int tiempo()
        {
            return Lista.Sum(el => el.Horas);
        }
        public void optimizar()
        {
            Lista=new Queue<Tarea>(Lista.OrderBy(el => el.Horas));
        }
        public override string ToString()
        {
           
            return String.Join(",", Lista);
        }

        public IEnumerator<Tarea> GetEnumerator()
        {
            return Lista.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Lista.GetEnumerator(); ;
        }
    }
}
