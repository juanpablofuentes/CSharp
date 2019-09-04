using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCBasic
{
    public class HolaMundoController : Controller
    {
        //
        //GET: /HolaMundo/

        public string Index()
        {
            return "Hola que tal?";
        }

        //
        //GET: /HolaMundo/Pro

        public string Pro()
        {
            return "Hola maquina ¿qué tal?";
        }
        //GEt: /HolaMundo/Personal
        public string Personal(string nombre)
        {
            return $"Hola {nombre}, ¿Qué pasa, figura?";
        }

        public IActionResult Vista()
        {
            return View();
        }


        public IActionResult VistaPersonal(string nombre="Anonimo")
        {
            ViewData["nombre"] = nombre;
            return View();
        }
    }
}