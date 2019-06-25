using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea
{
    public abstract class ElementoIkea<TContents> where TContents : IHerramientas, IPartes, new()

    {
        TContents articulo;
        public ElementoIkea()
        {
            // La obligación de new() nos permite hacer esto
            articulo = new TContents();
        }
        public abstract string Titulo
        {
            get;
        }

        public abstract string Color
        {
            get;
        }

        public void GetInventario()
        {
           
            foreach (string tool in articulo.GetHerramientas())
            {
                Console.WriteLine("Herramienta: {0}", tool);
            }
            foreach (string part in articulo.GetPartes())
            {
                Console.WriteLine("Componente: {0}", part);
            }
        }
    }
}
