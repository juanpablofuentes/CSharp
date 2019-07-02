using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basicas
{
    class Punto:IComparable<Punto>
    {
        private double x;
        private double y;

        public double Y
        {
            get { return y; }
            set { y = value; }
        }


        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Modulo
        {
            get { return Math.Sqrt(x * x + y * y); }
        }
        public Punto(double x, double y)
        {
            X = x;
            Y = y;
        }
        //Sobrecarga to string
        public override string ToString()
        {
            return $"[{X},{Y}]";
        }
        //Sobrecarga equals
        public override bool Equals(object obj)
        {
            if (!(obj is Punto)) return false;

            Punto p = (Punto)obj;
            return X == p.X && Y == p.Y;
        }
        //Sobrecarga hashcode (igual punto tendrá igual hashcode)
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public int CompareTo(Punto other)
        {
            if (this.Modulo > other.Modulo) return 1;
            if (this.Modulo < other.Modulo) return -1;
            return 0;
        }

        //SObrecarga operador igual
        public static bool operator ==(Punto obj1, Punto obj2)
        {
            return (obj1.Equals(obj2));
        }
        //SObrecarga operador distinto (obligatorio si sobrecargamos el = y viceversa)
        public static bool operator !=(Punto obj1, Punto obj2)
        {
            return !(obj1.Equals(obj2));
        }
        //Sobrecarga operaciones (el += y el -= está incluídas)
        public static Punto operator +(Punto p1, Punto p2) => new Punto(p1.X + p2.X, p1.Y + p2.Y);
      
        public static Punto operator -(Punto p1, Punto p2) => new Punto(p1.X - p2.X, p1.Y - p2.Y);

        // Sobrecarga ++ y -- sumando o restando la unidad a las dos coordenadas
        public static Punto operator ++(Punto p1) => new Punto(p1.X + 1, p1.Y + 1);
        
        public static Punto operator --(Punto p1) => new Punto(p1.X - 1, p1.Y - 1);

        public static Punto operator *(Punto p1, double n) => new Punto(p1.X*n, p1.Y*n);
        public static Punto operator *(Punto p1, Punto p2) => new Punto(p1.X * p2.Y, p1.Y * p2.X);

        public static Punto operator /(Punto p1, double n) => new Punto(p1.X/n, p1.Y/n);

        // Sobrecarga de las comparaciones
        public static bool operator <(Punto p1, Punto p2) => p1.CompareTo(p2) < 0;
        public static bool operator >(Punto p1, Punto p2) => p1.CompareTo(p2) > 0;
        public static bool operator <=(Punto p1, Punto p2) => p1.CompareTo(p2) <= 0;
        public static bool operator >=(Punto p1, Punto p2) => p1.CompareTo(p2) >= 0;
    }
}
