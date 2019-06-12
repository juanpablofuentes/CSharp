using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giros
{
    class Vehiculo : IVelocidad
    {
        private string pNombre;

        public string Nombre
        {
            get { return pNombre; }
            set { pNombre = value; }
        }
        public Vehiculo(string nombre)
        {
            Nombre = nombre;
        }
        public string girarDerecha()
        {
            return "Girando un vehículo a la derecha";
        }

        public string girarIzquierda()
        {
            return "Girando un vehículo a la izquierda"; 
        }

        public string acelerar()
        {
            return "Acelerando";
        }

        public string frenar()
        {
            return "Frenando";
        }
    }
}
