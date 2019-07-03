using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto
{
    class Producto
    {
        public string Nombre { get; set; }
        public double Precio { get; set; }
        
        public Producto(string nombre, double precio)
        {
            Nombre = nombre;
            Precio = precio;
        }
        public void cambiarPrecio(cambiar metodo)
        {
            foreach(cambiar m in metodo.GetInvocationList())
            {
                Precio = m(Precio);
            }
           
        }
        public override string ToString()
        {
            return Nombre +" "+ Precio.ToString("0.00");
        }
    }
}
