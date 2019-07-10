using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basicas
{
    class ListadeBlobs:IEnumerable<Blob>
    {
        private List<Blob> lista = new List<Blob>();
        public void agregar(Blob p)
        {
            lista.Add(p);
         }
        public Blob this[int index]
        {
            get { if (index < lista.Count) { return lista[index]; } else { return null; } }
            set { lista[index] = value; }
        }
        public IEnumerator<Blob> GetEnumerator()
        {
            return lista.FindAll(el=>el.ATAQUE>10).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return lista.GetEnumerator();
        }
        public void quitar(Blob p)
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
        public static ListadeBlobs operator +(ListadeBlobs lb, Blob b)
        {
            lb.agregar(b);
            return lb;
        }
        public static ListadeBlobs operator +(ListadeBlobs lb, int n)
        {
            lb.agregar(new Blob(n,n));
            return lb;
        }
        public static ListadeBlobs operator +(ListadeBlobs lb, ListadeBlobs lista)
        {
            foreach(Blob b in lista)
            {
                Console.WriteLine(b);
                lb.agregar(b);
            }
            return lb;
        }
    }
}
