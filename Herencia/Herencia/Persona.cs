using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Herencia
{
    abstract class Persona
    {
        private string _nombre;

        public Persona(string nombre)
        {
            Console.WriteLine("Constructor persona " + nombre);
            this.Nombre = nombre;
        }
        public string Nombre
        {
            set { this._nombre = value; }
            get { return this._nombre; }
        }
        public virtual string saludo()
        {
            return "Hola "+Nombre+" que tal";
        }
        //public override bool Equals(object obj)
        //{
        //    Persona p = (Persona)obj;
        //    Console.WriteLine(p.GetType());
        //    if (this.Nombre==p.Nombre && this.GetType() == p.GetType())
        //    {
        //        return true;
        //    }
        //    return false;

        //}

    }

    class Empleado :Persona
    {
        public Empleado(string nombre, int sueldo=0) : base(nombre)
        {
            Console.WriteLine("Constructor empleado " + nombre);

            this.Cargo = "Empleado";
        }
        public int Sueldo { get; set; }
        private string _cargo;
        public string Cargo {
            set { this._cargo = value; }
            get { return this._cargo; } }
        public override string saludo()
        {
            return "Hola empleado " + Nombre + " ¿Todo bien?";
        }
        public virtual double sueldoNeto()
        {
            return Sueldo * .85;
        }
        public double sueldoAnual()
        {
            return Sueldo * 12;
        }
       
    }
    class Gerente : Empleado
    {
        public int Bono { get; set; }
        public int Dietas { get; set; }
        public Gerente(string nombre) : base(nombre)
        {
            Console.WriteLine("Constructor gerente " + nombre);

            this.Cargo = "Gerente";

        }

        public Gerente(string nombre, int sueldo) : base(nombre, sueldo) { }
        public Gerente(string nombre, int sueldo, int dietas) : this(nombre, sueldo)
        {
            Dietas = dietas;
        }
        public override string saludo()
        {
            return "Hola Sr. " + Nombre + " ¿Desea alguna cosa?";
        }
        public override double sueldoNeto()
        {
            return base.sueldoNeto()+Dietas;
        }
        public new double  sueldoAnual()
        {
            return base.sueldoAnual() + Dietas * 12;
        }
    }
     class Direccion : Gerente
    {
        public int StockOptions { get; set; }
        public Direccion(string nombre) : base(nombre, 100) {
            Console.WriteLine("Constructor direccion " + nombre);
        }
        public Direccion(string nombre, int sueldo) : base(nombre, sueldo) { }
        public Direccion(string nombre, int sueldo, int dietas):base(nombre, sueldo, dietas) { }
        public Direccion(string nombre, int sueldo, int dietas, int stockoptions) : this(nombre, sueldo, dietas)
        {
            this.StockOptions = stockoptions;
        }
        public override string saludo()
        {

            return "Buenos días Sr. " + Nombre + " Estamos a sus órdenes";
        }
        public sealed override double sueldoNeto()
        {
            return base.sueldoNeto()+StockOptions*.5;
        }
    }
    class Jefazo : Direccion
    {
        public Jefazo(string nombre) : base(nombre) { }
    
    }
    class Cliente : Persona
    {
        public Cliente(string nombre) : base(nombre)
        {
        }
    }


}
