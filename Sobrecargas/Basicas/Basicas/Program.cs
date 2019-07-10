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

            ListadeBlobs lb = new ListadeBlobs();
            ListadeBlobs lb2 = new ListadeBlobs();
            lb.agregar(new Blob(2, 10));
            lb.agregar(new Blob(12, 1));
            lb.agregar(new Blob(22, 110));
            lb.agregar(new Blob(29, 310));
            foreach(Blob b in lb)
            {
                Console.WriteLine(b);
            }
            lb[0] = new Blob(10, 10);
            Console.WriteLine(lb[0]);
            lb=lb+ new Blob(10, 10);
            lb += 7;
            lb2 += 19;
            lb2 += 13;
            lb += lb2;
            Console.WriteLine(lb2);
            Console.WriteLine(lb);
            return;

            Punto p = new Punto(2, 7);
            Console.WriteLine(p);
            Punto q = new Punto(2, 7);
            Console.WriteLine(p.Equals(q));
            Console.WriteLine(p == q);
            Console.WriteLine(p.GetHashCode());
            Console.WriteLine(q.GetHashCode());
            
            Punto r = p + q;
            Console.WriteLine(r);
            r -= p;
            Console.WriteLine(r);
            r++;
            Console.WriteLine(r);
            r = 2 * r;
            Console.WriteLine(r);
            r *= p;
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
