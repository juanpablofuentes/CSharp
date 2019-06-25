using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea
{
    public class Gronstrum : ElementoIkea<Silla>
    {
        public override string Titulo
        {
            get { return "Gronstrum"; }
        }


        public override string Color
        {
            get { return "Cyan"; }
        }
    }

    public class Frinstrom : ElementoIkea<Silla>
    {
        public override string Titulo
        {
            get { return "Frinstrom"; }
        }


        public override string Color
        {
            get { return "Yellow"; }
        }
    }

    public class Kraokt : ElementoIkea<Armario>
    {
        public override string Titulo
        {
            get { return "Kraokt"; }
        }


        public override string Color
        {
            get { return "Black"; }
        }
    }
}
