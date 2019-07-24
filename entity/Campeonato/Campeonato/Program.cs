using System;
using Campeonato.Models;

namespace Campeonato
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new campeonatoContext())
            {
                Jugador pepe = new Jugador();
                pepe.Nombre = "Pepe";
                pepe.Puntos = 5;
                contexto.Jugador.Add(pepe);
                Partido p = new Partido();
                p.Fecha = DateTime.Parse( "2019-8-19");
                pepe.Partido.Add(p);
                contexto.SaveChanges();
            }
        }
    }
}
