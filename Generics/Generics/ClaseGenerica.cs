using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    class ClaseGenerica<T>
    {
        private T variableTipoGenerico;

        public ClaseGenerica(T valor)
        {
            variableTipoGenerico = valor;
        }

        public T metodoGenerico(T paramGenerico)
        {
            Console.WriteLine("Tipo parámetro: {0}, valor: {1}", typeof(T).ToString(), paramGenerico);
            Console.WriteLine("Tipo return: {0}, valor: {1}", typeof(T).ToString(), variableTipoGenerico);

            return variableTipoGenerico;
        }

        public T propiedadGenerica { get; set; }
    }
}
