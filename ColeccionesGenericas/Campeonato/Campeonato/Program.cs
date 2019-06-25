using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato
{
    class Program
    {
        static void Main(string[] args)
        {
            Liga l = new Liga();
            l.add(new Equipo("Logroñés"));
            l.add(new Equipo("Numancia"));
            l.add(new Equipo("Albacete"));
            l.add(new Equipo("Sabadell"));
            Console.WriteLine(l);
            List<Partido> partidos = l.partidos();
            partidos.ForEach(Console.WriteLine);
        }
    }
}
