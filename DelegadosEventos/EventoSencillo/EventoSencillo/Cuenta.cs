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
        public delegate void aviso(string mensaje);
        public event aviso alertas;


    }
}
