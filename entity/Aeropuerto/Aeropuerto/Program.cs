using System;

namespace Aeropuerto
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Contexto())
            {
                Piloto ana = new Piloto();
                ana.Nombre = "Ana";
                context.Pilotos.Add(ana);
                Avion jumbo = new Avion
                {
                    Nombre = "Jumbo 2"
                };
                context.Aviones.Add(jumbo);
                Vuelo soria = new Vuelo
                {
                    IdAvion = 2,
                    IdPiloto = 2,
                    Fecha = DateTime.Now
                };
                Vuelo mallorca = new Vuelo();
                mallorca.Avion = jumbo;
                mallorca.Piloto = ana;
                mallorca.Fecha = DateTime.Now;
                context.Vuelos.Add(soria);
                context.Vuelos.Add(mallorca);
                context.SaveChanges();
            }
        }
    }
}
