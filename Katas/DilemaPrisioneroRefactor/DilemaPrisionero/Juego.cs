using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DilemaPrisionero
{
    class Juego
    {
        public List<Jugador> Jugadores = new List<Jugador>();
        private List<Enfrentamiento> enfrentamientos = new List<Enfrentamiento>();
        private int rondas = 50;
        private Pagos evaluacion ;
        
        public Juego(List<Jugador> jugadores, Pagos pagos=null, int numRondas=50)
        {
            if (jugadores.Count < 2)
            {
                throw new Exception("Jugadores insuficientes");
            }
            Jugadores = jugadores;
            if (pagos == null)
            {
                evaluacion = new PagoClasico();
            }
            else
            {
                evaluacion = pagos;
            }
            rondas = numRondas;
        }
        private void crearEnfrentamientos()
        {
            enfrentamientos.Clear();
            for(int i = 0; i < Jugadores.Count - 1; i++)
            {
                for(int j = i + 1; j < Jugadores.Count; j++)
                {
                    enfrentamientos.Add(new Enfrentamiento(Jugadores[i], Jugadores[j], evaluacion,rondas));
                }
            }
        }
        private void enfrentar()
        {
            foreach(Enfrentamiento enf in enfrentamientos)
            {
                enf.jugar();
            }
        }
        public void jugar()
        {
            crearEnfrentamientos();
            enfrentar();
            
        }
        public override string ToString()
        {
            Jugadores.Sort(new ordenDinero());
            string res = "";
            foreach(Jugador j in Jugadores)
            {
                res += j+"\r\n";
            }
            return res;
        }
    }
    class JuegoQueue
    {
        public List<Jugador> Jugadores = new List<Jugador>();
        private Queue<Enfrentamiento> enfrentamientos = new Queue<Enfrentamiento>();
        private int rondas = 50;
        private Pagos evaluacion ;
        public JuegoQueue(List<Jugador> jugadores, Pagos pagos = null, int numRondas = 50)
        {
            if (jugadores.Count < 2)
            {
                throw new Exception("Jugadores insuficientes");
            }
            Jugadores = jugadores;
            if (pagos == null)
            {
                evaluacion = new PagoClasico();
            }
            else
            {
                evaluacion = pagos;
            }
            rondas = numRondas;
        }
        private void crearEnfrentamientos()
        {
            enfrentamientos.Clear();
            for (int i = 0; i < Jugadores.Count - 1; i++)
            {
                for (int j = i + 1; j < Jugadores.Count; j++)
                {
                    enfrentamientos.Enqueue(new Enfrentamiento(Jugadores[i], Jugadores[j], evaluacion, rondas));
                }
            }
        }
        private void enfrentar()
        {
            foreach (Enfrentamiento enf in enfrentamientos)
            {
                enf.jugar();
            }
        }
        public void jugar()
        {
            crearEnfrentamientos();
            enfrentar();

        }
        public override string ToString()
        {
            Jugadores.Sort(new ordenDinero());
            string res = "";
            foreach (Jugador j in Jugadores)
            {
                res += j + "\r\n";
            }
            return res;
        }
    }

    public class ordenDinero : IComparer<Jugador>
    {
         int IComparer<Jugador>.Compare(Jugador x, Jugador y)
        {
            if (x.Dinero > y.Dinero) return -1;
            if (x.Dinero < y.Dinero) return 1;
            return 0;
        }

    }
}
