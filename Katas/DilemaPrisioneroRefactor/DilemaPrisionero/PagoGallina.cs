using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DilemaPrisionero
{
    class PagoGallina : Pagos
    {
        public PagoGallina()
        {
            Matriz = new Pago[2, 2]{
                                {new Pago(-20,-20), new Pago(10,1)},
                                {new Pago(1,10), new Pago(5,5)}
                            };
        }
    }
}
