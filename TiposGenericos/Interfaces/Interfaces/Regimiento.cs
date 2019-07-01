using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    class Regimiento<T> where T:IGiros
    {
        List<T> lista = new List<T>();
        public void add(T pieza)
        {
            lista.Add(pieza);
        }
        public void girarDerecha()
        {
            foreach(T el in lista)
            {
                el.girarDerecha();
            }
        }
        public void girarIzquierda()
        {
            foreach (T el in lista)
            {
                el.girarIzquierda();
            }
        }
    }
}
