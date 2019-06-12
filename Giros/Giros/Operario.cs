using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giros
{
    class Operario
    {
        private IGiros aparato;
        public Operario(IGiros aparato)
        {
            this.aparato = aparato;
        }
        public void trompo()
        {
            for(int i = 0; i < 4; i++)
            {
                Console.WriteLine(this.aparato.girarDerecha());
                
            }
        }
    }
}
