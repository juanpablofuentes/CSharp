using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea
{
    class Program
    {
        static void Main(string[] args)
        {

            Kraokt g = new Kraokt();

            Console.WriteLine(g.Titulo);
            g.GetInventario();

        }


    }
}
