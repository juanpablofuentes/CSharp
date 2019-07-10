using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basicas
{
    class Puntos : IEnumerable<Punto>
    {
        private List<Punto> lista = new List<Punto>();
        public void agregar(Punto p)
        {
            lista.Add(p);
        }



        public void quitar(Punto p)
        {
            if (lista.Contains(p))
            {
                lista.Remove(p);
            }
        }
        public override string ToString()
        {
            return String.Join(",", lista);
        }
        public Punto this[int index]
        {
            get { return lista[index]; }
            set { lista.Insert(index, value); }
        }
   
        public IEnumerator<Punto> GetEnumerator()
        {
            return lista.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return lista.GetEnumerator();
        }
        public static Puntos operator +(Puntos p1, Punto p2)
        {
            p1.agregar(p2);
            return p1;
        }
        public static Puntos operator -(Puntos p1, Punto p2)
        {
            p1.quitar(p2);
            return p1;
        }
    }
}
