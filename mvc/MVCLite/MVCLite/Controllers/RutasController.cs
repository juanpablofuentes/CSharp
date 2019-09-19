using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCLite.Controllers
{
    public class RutasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Tabla(int lado)
        {
            ViewBag.lado = lado;
            return View();
        }
        [Route("/fijo")]
        public IActionResult Anotacion()
        {
            ViewBag.lado = 10;
            return View("Tabla");
        }
        [Route("/saludo/{nombre}")]
        public IActionResult Saludo(string nombre)
        {
            ViewBag.nombre = nombre;
            return View();
        }
    }
}