using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DilemaPrisionero
{
    class PagoAmigoEnemigo : Pagos
    {
        public PagoAmigoEnemigo()
        {
            Matriz = new Pago[2, 2]{
                                {new Pago(0,0), new Pago(2,0)},
                                {new Pago(0,2), new Pago(1,1)}
                            };
        }
    }
}
