using System;
using System.Collections.Generic;
using System.Text;

namespace Comics
{
    class Menus
    {
        private string[] Secciones = new string[] { "1 - Categorías","2 - Autores", "3 - Cómics","0 - Salir" };
        private string[] Acciones = new string[] { "1 - Añadir","2 - Ver", "3 - Actualizar","4 - Eliminar", "0 - Salir" };

        private void mostrar(string[] opciones)
        {
            foreach(string opcion in opciones)
            {
                Console.WriteLine(opcion);
            }
        }
        public int getOpcion(string[] opciones)
        {
            int opcion;
            do
            {
                mostrar(opciones);
                int.TryParse(Console.ReadLine(), out opcion);
            } while (opcion < 0 || opcion >= opciones.Length);
            return opcion;
        }
        public int getSeccion()
        {
            return getOpcion(Secciones);
        }
        public int getAccion()
        {
            return getOpcion(Acciones);
        }
    }
}
