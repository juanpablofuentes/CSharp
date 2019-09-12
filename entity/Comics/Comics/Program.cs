using System;
using System.Reflection;

namespace Comics
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] methods = new string[] { "create", "read", "update", "delete" };
            using (Contexto contexto = new Contexto())
            {
                int seccion, accion;
                Menus menu = new Menus();
                CRUD Entidad=null;
                while (true)
                {
                    seccion = menu.getSeccion();
                    if (seccion == 0) break;
                    accion = menu.getAccion();
                    if (accion == 0) break;
                    switch (seccion)
                    {
                        case 1:
                            Entidad = new CategoriaCRUD(contexto);
                            break;
                        case 2:
                            Entidad = new AutorCRUD(contexto);
                            break;
                        case 3:
                            Entidad = new ComicCRUD(contexto);
                            break;
                    }
                    //Entidad= (CRUD) Activator.CreateInstance(Type.GetType("CategoriaCRUD"), contexto);
                    
                    MethodInfo mi = Entidad.GetType().GetMethod(methods[accion-1]);
                    mi.Invoke(Entidad, null);
                    
                }
            }
        }

    }
}
