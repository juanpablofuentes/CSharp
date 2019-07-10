using System;

namespace EmpresaDespidos
{
    class Program
    {
        static void Main(string[] args)
        {
            Empresa e = new Empresa();
            Empleado a = new Empleado("aa");
            Empleado b = new Empleado("bb");
            Empleado c = new Empleado("cc");
            Empleado d = new Empleado("dd");
            e.contratar(a);
            e.contratar(b);
            e.contratar(c);
            e.contratar(d);
            e.despido += gestionar;
            //Añadir dos suscripciones a despido
            //La primera nos muestra por consola el texto 'Delegado anónimo' y la hacemos con un delegado anónimo
            //La segunda nos muestra por consola el texto 'Función lambda' y la hacemos con una función lambda
            e.despedir(b);
        }
        static void gestionar(Empleado emp)
        {
            Console.WriteLine(emp.Nombre+" estás despedido ¡fuera de la empresa!");
        }
    }
}
