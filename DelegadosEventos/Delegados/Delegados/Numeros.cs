using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegados
{
    class Numeros
    {
        public List<double> lista = new List<double>();

        public void procesar(operacion op)
        {
            for(int i = 0; i < lista.Count; i++)
            {
                lista[i] = op(lista[i]);
            }
        }

        public override string ToString()
        {
            return String.Join(",", lista);
        }
    }
}
