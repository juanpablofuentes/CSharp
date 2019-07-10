using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basicas
{
    class Test01
    {
        private int valor;

     

        public int Valor
        {
            get { return valor; }
            set { valor = value; }
        }
        public Test01(int valor)
        {
            Valor = valor;
        }
        public override bool Equals(object obj)
        {
            if (obj is Test01 v)
            {
                return Valor == v.Valor;
            }
            return false;
        }
    }

    class Test02
    {
        private int valor;



        public int Valor
        {
            get { return valor; }
            set { valor = value; }
        }
        public Test02(int valor)
        {
            Valor = valor;
        }
        public override bool Equals(object obj)
        {
            if (obj is Test01 v)
            {
                return Valor == v.Valor;
            }
            return false;
        }
        public static bool operator ==(Test02 obj1, Test02 obj2)
        {
            return (obj1.Equals(obj2));
        }
        //sobreescritura operador distinto (obligatorio si sobreescrituramos el = y viceversa)
        public static bool operator !=(Test02 obj1, Test02 obj2)
        {
            return !(obj1.Equals(obj2));
        }
        public override int GetHashCode()
        {
            var hashCode = 393507274;
            hashCode = hashCode * -1521134295 + valor.GetHashCode();
            hashCode = hashCode * -1521134295 + Valor.GetHashCode();
            return hashCode;
        }
    }
}
