using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayGenerics
{
    class Coleccion<T>
    {
        private List<T> elementos=new List<T>();

        public void add(T elemento)
        {
            elementos.Add(elemento);
        }
        
        public void mostrar()
        {
            foreach(T el in elementos)
            {
                Console.WriteLine(el);
            }
        }
        
    }
}
