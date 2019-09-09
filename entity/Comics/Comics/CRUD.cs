using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comics
{
    public abstract class CRUD
    {
        protected Contexto _contexto;

        public CRUD(Contexto contexto)
        {
            _contexto = contexto;
        }
        public abstract bool create();
        public abstract bool read();
        public abstract bool update();
        public abstract bool delete();
    }
}
