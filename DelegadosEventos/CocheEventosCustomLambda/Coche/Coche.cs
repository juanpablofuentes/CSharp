using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coche
{
    class Coche
    {
        // Internal state data.
        public int Velocidad { get; set; }
        public int Limite { get; set; } = 100;
        public string Nombre { get; set; }

        // ¿Marcha el coche?
        private bool estaMuerto;

        // Class constructors.
        public Coche() { }
        public Coche(string nombre, int limite, int velocidad)
        {
            Velocidad = velocidad;
            Limite = limite;
            Nombre = nombre;
        }

     
        // 1) Definir el delegado.
        public delegate void ManejarMotor(object sender, ArgumentoEventosCoche e);

        // 2) Eventos.
        public event ManejarMotor Explotar;
        public event ManejarMotor AlLimite;

      

        // 4) Implement the Accelerate() method to invoke the delegate’s 
        //    invocation list under the correct circumstances.
        public void Acelerar(int delta)
        {
            // If this car is 'dead', send dead message.
            if (estaMuerto)
            {
               
                    Explotar?.Invoke(this, new ArgumentoEventosCoche("El coche tiene muerto el motor..."));
            }
            else
            {
                Velocidad += delta;

                // Is this car 'almost dead'?
                if (10 == (Limite - Velocidad))
                {
                 
                    AlLimite?.Invoke(this, new ArgumentoEventosCoche("Cuidado que explota"));
                }

                if (Velocidad >= Limite)
                    estaMuerto = true;
                else
                    Console.WriteLine("Velocidad = {0}", Velocidad);
            }
        }
        public void restaurar()
        {
            Velocidad = 0;
            estaMuerto = false;
        }
  
    }
}
