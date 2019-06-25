using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v1
{
    class Jugador
    {
        public string Nombre { get; set; }
        public string jugada { get; set; }
        public Jugador(string nombre)
        {
            this.Nombre = nombre;
        }
        public void pedirJugada(string[] validas)
        {
            do
            {
                Console.WriteLine(this.Nombre + " introduzca jugada");
                this.jugada = Console.ReadLine();
            } while (!validas.Contains(this.jugada));
        }
    }
}
