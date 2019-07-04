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
            
            e.despedir(b);
        }
        static void gestionar(Empleado emp)
        {
            Console.WriteLine(emp.Nombre+" estás despedido ¡fuera de la empresa!");
        }
    }
}
