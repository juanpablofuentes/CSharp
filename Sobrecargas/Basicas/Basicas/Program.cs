using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basicas
{
    class Program
    {
        static void Main(string[] args)
        {
            Punto p = new Punto(2, 7);
            Console.WriteLine(p);
            Punto q = new Punto(2, 7);
            Console.WriteLine(p.Equals(q));
            Console.WriteLine(p == q);
            Punto r = p + q;
            Console.WriteLine(r);
            r -= p;
            Console.WriteLine(r);
            r++;
            Console.WriteLine(r);
            Console.WriteLine(r > p);
            Console.WriteLine(p > q);
            Console.WriteLine(p >= q);
            r = r * 4;
            Console.WriteLine(r);
            Puntos lp = new Puntos();
            lp.agregar(p);
            lp.agregar(q);
            lp.agregar(r);
            Console.WriteLine(lp);
            Punto s = new Punto(12, 32);
            lp.quitar(s);
            Console.WriteLine(lp);
            foreach(Punto w in lp)
            {
                Console.WriteLine(w);
            }
            Console.WriteLine(lp[0]);
            lp[0] = s;
            Console.WriteLine(lp[0]);
            lp += r;
            Console.WriteLine(lp);
            lp -= r;
            Console.WriteLine(lp);
            
        }
    }
}
