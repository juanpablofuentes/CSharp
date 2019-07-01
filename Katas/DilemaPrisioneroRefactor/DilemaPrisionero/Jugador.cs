using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DilemaPrisionero
{
    class Jugador
    {
        private string _nombre;
        private IEstrategia _estrategia;
        private int dinero;

        public int Dinero
        {
            get { return dinero; }
            set { dinero = value; }
        }

        public IEstrategia Estrategia
        {
            get { return _estrategia; }
            set { _estrategia = value; }
        }


        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public Jugador(string nombre, IEstrategia estrategia)
        {
            Nombre = nombre;
            Estrategia = estrategia;
            Dinero = 0;
        }
        public int decision(List<int> historial=null)
        {
            return _estrategia.decision(historial);
        }
        public override string ToString()
        {
            return Nombre + " ( " + Dinero + "€ )";
        }
    }
}
