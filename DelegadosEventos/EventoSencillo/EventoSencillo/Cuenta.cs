using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventoSencillo
{
    class Cuenta
    {
        private int saldo;

        public int Saldo
        {
            get { return saldo; }
            set { saldo = value;  alertas?.Invoke("Saldo actual "+ value); }
        }
        //Aquí definimos el delegado, es decir, la firma de las funciones
        //que se podrán suscribir a este evento
        public delegate void aviso(string mensaje);
        //Aquí definimos el evento 'alertas' del tipo 'aviso'
        public event aviso alertas;


    }
}
