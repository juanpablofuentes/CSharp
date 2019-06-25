using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DilemaPrisionero
{
    class Program
    {
        static void Main(string[] args)
        {
            Jugador Panfilo = new Jugador("Panfilo", new confiado());
            Jugador Maquiavelo = new Jugador("Maquiavelo", new aprovechado());
            Jugador Tuntun = new Jugador("Tuntún", new aleatorio());
            Jugador donde = new Jugador("Donde las dan las toman", new dondelasdan());
            Jugador donde2 = new Jugador("Donde las dan las toman", new dondelasdan());
            Jugador donde3 = new Jugador("Donde las dan las toman", new dondelasdan());
            //Enfrentamiento ronda = new Enfrentamiento(donde, Tuntun, new Pagos() );
            //ronda.jugar();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //Console.WriteLine(ronda);
            //Console.WriteLine(donde.Dinero);
            Juego dilema = new Juego();
            dilema.Jugadores.Add(Panfilo);
            dilema.Jugadores.Add(Maquiavelo);
            dilema.Jugadores.Add(Tuntun);
            dilema.Jugadores.Add(donde);
            dilema.Jugadores.Add(donde2);
            dilema.Jugadores.Add(donde3);
            dilema.jugar();
            Console.WriteLine(dilema);
        }
    }
}
