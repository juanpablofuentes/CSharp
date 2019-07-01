using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas
{
    class EstadoVentas
    {
        private IDictionary<string, int> lista;
        public EstadoVentas()
        {
            lista = new Dictionary<string, int>();
        }
        public void agregarVenta(string empleado, int ventas)
        {
            if (lista.ContainsKey(empleado))
            {
                lista[empleado] += ventas;
            }
            else
            {
                lista[empleado] = ventas;
            }
        }
        public int totalVentas()
        {
            return lista.Sum(el => el.Value);
        }
        public double mediaVentas()
        {
            return (double)totalVentas() / (double)lista.Count;
        }
        public string mejorVendedor()
        {
            string mejor = lista.Keys.ElementAt(0);
            int venta = lista.Values.ElementAt(0);
            foreach(string v in lista.Keys)
            {
                if (lista[v] > venta)
                {
                    mejor = v;venta = lista[v];
                }
            }
            return mejor;
        }
        public string mejorVendedorLinq()
        {
            return lista.OrderByDescending(entry => entry.Value).First().Key;
        }
        public void despedir(string vendedor)
        {
            if (lista.ContainsKey(vendedor))
            {
                lista.Remove(vendedor);
            }
        }
        public override string ToString()
        {
            return String.Join(",", lista);
        }
    }
}
