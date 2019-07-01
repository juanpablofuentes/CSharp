using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaTelefonos
{
    class Agenda
    {
        private SortedList<string, string> lista = new SortedList<string, string>();
        public void agregarContacto(string nombre, string telefono)
        {
            lista[nombre] = telefono;
        }
        public override string ToString()
        {
            return String.Join(",", lista);
        }
        public List<string> nombre(string telefono)
        {
            List<string> nombres = new List<string>();
            foreach(KeyValuePair<string,string> el in lista)
            {
                if (el.Value == telefono)
                {
                    nombres.Add(el.Key);
                }
            }
            return nombres;
        }
        public List<string> repetidos()
        {
            List<string> valores = new List<string>();
            foreach(string v in lista.Values)
            {
                if (nombre(v).Count > 1)
                {
                    valores.AddRange(nombre(v));
                }
            }
            return valores.Distinct().ToList();

        }
    }
}
