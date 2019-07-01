using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DilemaPrisionero
{
    class PagoClasico : Pagos
    {
        public PagoClasico()
        {
            Matriz = new Pago[2, 2]{
                                {new Pago(-1,-1), new Pago(5,-5)},
                                {new Pago(-5,5), new Pago(3,3)}
                            };
        }
    }
}
