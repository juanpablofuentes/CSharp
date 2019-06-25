using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DilemaPrisionero
{
    class Enfrentamiento
    {
        private Jugador Jugador1;
        private Jugador Jugador2;
        List<int> historial1 = new List<int>();
        List<int> historial2 = new List<int>();
        List<Pago> historialPagos = new List<Pago>();
        private Pagos Calculo;
        private int Rondas;
        public Enfrentamiento(Jugador jugador1, Jugador jugador2, Pagos calculo, int rondas=50)
        {
            Jugador1 = jugador1;
            Jugador2 = jugador2;
            Calculo = calculo;
            Rondas = rondas;
        }
        public void jugar()
        {
            for(int i = 0; i < Rondas; i++)
            {
                int jug1 = Jugador1.decision(historial2);
                int jug2 = Jugador2.decision(historial1);
                Pago p = Calculo.Evaluar(jug1, jug2);
                historial1.Add(jug1);
                historial2.Add(jug2);
                historialPagos.Add(p);
                Jugador1.Dinero += p.jug1;
                Jugador2.Dinero += p.jug2;
            }
        }
        public override string ToString()
        {
            string res;
            res = Jugador1.Nombre + " vs. " + Jugador2.Nombre + "\r\n";
            res += "( " + Jugador1.Dinero + "€ )  --  (" + Jugador2.Dinero + "€ ) \r\n";
            for (int i = 0; i < historial1.Count; i++)
            {
                res += "( " + historial1[i] + ") " + historialPagos[i].jug1 + "€ ( " + historial2[i] + ") " + historialPagos[i].jug2 + "€ \r\n";
            }
            return res;
        }
    }
}
