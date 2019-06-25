using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Herencia
{
    class Program
    {
        static void Main(string[] args)
        {
           // Persona p = new Persona("PP");
            Empleado eva = new Empleado("Eva",1200);
            Empleado eruiz = new Empleado("Eva", 1200);


            Gerente ana = new Gerente("Ana");
            ana.Sueldo = 1700;
            ana.Dietas = 200;

            Empleado e = eva;
            Console.WriteLine(e.Equals(eva));
            
            Direccion rosa = new Direccion("Rosa");
            rosa.Sueldo = 2500;
            rosa.Dietas = 300;
            rosa.StockOptions = 100;
            Console.WriteLine(rosa.sueldoNeto());
            Empleado[] plantilla = { eva, ana, rosa };
            double total = 0;
            foreach(Empleado item in plantilla)
            {
                total += item.sueldoNeto();
            }
            Console.WriteLine(eva.Equals(eruiz));
            Console.WriteLine(total);
            Funcionario alberto = new Funcionario("Alberto");
            alberto.turno(eva);
            alberto.turno(ana);
            alberto.turno(rosa);
            alberto.turno(eva);
            alberto.atender();
            alberto.atender();
            alberto.atender();
        }
    }
}
