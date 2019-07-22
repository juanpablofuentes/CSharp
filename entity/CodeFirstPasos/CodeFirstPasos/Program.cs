using System;

namespace CodeFirstPasos
{
    class Program
    {
        static void Main(string[] args)
        {
            //Añadir entityFramework nuGet
            //Crear entidades
            //Crear contexto
            //Crear enlace a sql server
            //Crear la base de datos    this.Database.Migrate();
            //or
            // Add-Migration CodeFirstPasos.Contexto2
            //Update-database
            using (var contexto = new Contexto())
            {
                 contexto.Database.EnsureCreated();
            }
        }
    }
}
