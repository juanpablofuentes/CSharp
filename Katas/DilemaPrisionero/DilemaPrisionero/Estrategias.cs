using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DilemaPrisionero
{
    class confiado : IEstrategia
    {
        public int decision(List<int> historial = null)
        {
            return 1;
        }
    }
    class aprovechado : IEstrategia
    {
        public int decision(List<int> historial = null)
        {
            return 0;
        }
    }
    class aleatorio : IEstrategia
    {
        public int decision(List<int> historial = null)
        {
            Random r = new Random(Convert.ToInt32(DateTime.Now.Subtract(DateTime.Today).Milliseconds));
            return r.Next(0, 2);
        }
    }
    class dondelasdan : IEstrategia
    {
        public int decision(List<int> historial = null)
        {
            if (historial == null || historial.Count == 0)
            {
                return 1;
            }
            else { return historial.Last(); }
        }
    }
}
