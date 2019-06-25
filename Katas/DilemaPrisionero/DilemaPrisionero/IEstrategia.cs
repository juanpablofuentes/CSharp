using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DilemaPrisionero
{
    public interface IEstrategia
    {
         int decision(List<int> historial = null);
    }
}
