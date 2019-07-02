using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            Regimiento<Tanque> r = new Regimiento<Tanque>();

            Regimiento<Torreta> t = new Regimiento<Torreta>();

            r.add(new Tanque());
            t.add(new Torreta());
        }
    }
}
