using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventoSencillo
{
    class Program
    {
        static void Main(string[] args)
        {
            Cuenta cc = new Cuenta();
            cc.alertas += avisoCliente;

            cc.Saldo += 900;
            cc.Saldo -= 90;
        }
        //Puedo suscribir este método porque tiene la firma del delegado
        static void avisoCliente(string mensaje)
        {
            Console.WriteLine(mensaje);
        }
     
    }
}
