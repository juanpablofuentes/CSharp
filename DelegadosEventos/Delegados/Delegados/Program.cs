using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegados
{
    public delegate double operacion(double value);
    public delegate void Imprimir(int value);
    public delegate void Empresa(string nombre);
    class Program
    {
        static void Main(string[] args)
        {
            Imprimir delegado = ImprimirNumero;
            Empresa e = bienvenida;
            Empleado eva = new Empleado("Eva");
            eva.ejecutar(e);
            e = despedido;
            eva.ejecutar(e);
            eva.ejecutar(promocionar);
            int num = 1000;

            delegado(num);
            delegado.Invoke(num);
            Console.WriteLine("------");
            delegado = ImprimirMoneda;
            delegado(num);
            delegado.Invoke(num);
            Console.WriteLine("------");

            delegado += ImprimirNumero;
            delegado += ImprimirHexa;

            delegado(num);
            Console.WriteLine("------");

            delegado -= ImprimirNumero;
            delegado(num);
            Console.WriteLine("------");

            e = bienvenida;
            e += promocionar;

            eva.ejecutar(e);


            Numeros n = new Numeros();
            n.lista.Add(10);
            n.lista.Add(20);
            n.lista.Add(30);
            n.lista.Add(40);
            n.lista.Add(50);
            operacion op;
            Console.WriteLine(n);
            Console.WriteLine("------");
            op = Operaciones.cuadrado;

            n.procesar(op);
            Console.WriteLine(n);
            Console.WriteLine("------");
            op = Operaciones.raiz;
            n.procesar(op);
            Console.WriteLine(n);
            Console.WriteLine("------");
            op = Operaciones.doble;
            op += Operaciones.cuadrado;
            n.procesar(op);
            Console.WriteLine(n);
            Console.WriteLine("------");
        }
        public static void bienvenida(string nombre)
        {
            Console.WriteLine("Hola " + nombre);
        }
        public static void despedido(string nombre)
        {
            Console.WriteLine(nombre + " estás despedido");
        }
         public static void promocionar(string nombre)
        {
            Console.WriteLine(nombre + " te hemos subido el sueldo");
        }
        public static void ImprimirNumero(int num)
        {
            Console.WriteLine("Numero: {0,-12:N0}", num);
        }
        public static void ImprimirNumero(double num)
        {
            Console.WriteLine("Numero: {0,-12:N0}", num);
        }
        public static void ImprimirMoneda(int money)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Moneda: {0:C} ", money);
        }

        public static void ImprimirHexa(int dec)
        {
            Console.WriteLine("Hexadecimal: {0:X}", dec);
        }

    }

}
