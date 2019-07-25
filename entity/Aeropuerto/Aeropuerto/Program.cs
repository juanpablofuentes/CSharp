using System;

namespace Aeropuerto
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Contexto())
            {
                //Piloto pepe = new Piloto();
                //pepe.Nombre = "Pepe";
                //context.Pilotos.Add(pepe);
                //Avion jumbo = new Avion();
                //jumbo.Nombre = "Jumbo";
                //context.Aviones.Add(jumbo);
                Vuelo soria = new Vuelo
                {
                    IdAvion = 2,
                    IdPiloto = 2,
                    Fecha = DateTime.Now
                };
                context.Vuelos.Add(soria);
                context.SaveChanges();
            }
        }
    }
}
