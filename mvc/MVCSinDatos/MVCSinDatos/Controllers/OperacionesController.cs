using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCSinDatos.Controllers
{
    public class OperacionesController : Controller
    {
        public string Index()
        {
            return "Hola que tal";
        }

        public string Suma(int a, int b)
        {
            return $"{a}+{b}={a + b}";
        }
    }
}