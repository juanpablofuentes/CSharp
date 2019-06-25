using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DilemaPrisionero
{
    public struct Pago
    {
        public int jug1, jug2;

        public Pago(int p1, int p2)
        {
            jug1 = p1;
            jug2 = p2;
        }
    }
    class Pagos
    {
        private Pago[,] _matriz;

        public Pago[,] Matriz
        {
            get { return _matriz; }
            set { _matriz = value; }
        }
        public Pagos()
        {
            Matriz = new Pago[2, 2]{
                                {new Pago(-1,-1), new Pago(5,-5)},
                                {new Pago(-5,5), new Pago(3,3)}
                            };
        }
        public Pago Evaluar(int p1,int p2)
        {
            return Pago[p1, p2];
        }
    }
}
