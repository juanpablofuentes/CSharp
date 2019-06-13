using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v3
{
    class Consola : IMostrar
    {
        public void mostrar(string res)
        {
            Console.WriteLine(res);


        }
    }
}
