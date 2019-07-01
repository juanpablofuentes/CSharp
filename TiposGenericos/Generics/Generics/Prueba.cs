using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    class Prueba<T>
    {
        T interna;
        public Prueba(T valor)
        {
            interna = valor;
        }
        public bool igual(T valor)
        {
            return interna.Equals(valor);
        }
    }
}
