using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato
{
    class Liga
    {
        private List<Equipo> equipos = new List<Equipo>();
        public void add(Equipo equipo)
        {
            if (!equipos.Contains(equipo))
            {
                equipos.Add(equipo);
            }
        }
        public void remove(Equipo equipo)
        {
            if (!equipos.Contains(equipo))
            {
                throw new Exception("El equipo no existe");
            } else
            {
                equipos.Remove(equipo);
            }
        }
        public List<Partido> partidos()
        {
            List<Partido> ida = new List<Partido>();
            List<Partido> vuelta = new List<Partido>();
           
            for (int i = 0; i < equipos.Count-1; i++)
            {
                for(int j = i + 1; j < equipos.Count ; j++)
                {
                    ida.Add(new Partido(equipos[j], equipos[i]));
                    vuelta.Add(new Partido(equipos[i], equipos[j]));
                }
            }
            ida.Sort(new shuffle());
            vuelta.Sort(new shuffle());
            ida.AddRange(vuelta);
            return ida;
        }
        public override string ToString()
        {
            return String.Join(",", equipos);
        }
       
    }
    public class shuffle : IComparer<Partido>
    {
        Random r = new Random(Convert.ToInt32(DateTime.Now.Subtract(DateTime.Today).TotalSeconds));
        // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer<Partido>.Compare(Partido x, Partido y)
        {
            
           
            return r.Next(0,3)-1;
        }

    }
}
