using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCLite.Controllers
{
    public class TablasController : Controller
    {
        [Route("/diagonal/{lado}")]
        public IActionResult Diagonal(int lado)
        {
            int[,] tabla = new int[lado, lado];
            for (int i = 0; i < lado; i++)
            {
                for (int j = 0; j < (lado - i); j++)
                {
                    tabla[j, j + i] = i + 1;
                    tabla[j + i, j] = i + 1;
                }
            }
            ViewBag.tabla = tabla;
            ViewBag.lado = lado;
            return View("Global");
        }
        [Route("/diamante/{lado}")]
        public IActionResult Diamante(int lado)
        {
            int[,] tabla = new int[lado, lado];
            for (int i = 0; i < lado/2+ lado % 2; i++)
            {
                for (int j = 0; j < lado/2+lado%2; j++)
                {
                   
                    tabla[ i, j] = i +j+ 1;
                    tabla[i,lado-j-1]= i + j + 1;
                    tabla[lado-i-1, j] = i + j + 1;
                    tabla[lado - i - 1, lado - j - 1] = i + j + 1;
                }
            }
            ViewBag.tabla = tabla;
            ViewBag.lado = lado;
            return View("Global");
        }
        [Route("/random/{lado}")]
        public IActionResult Random(int lado)
        {
            Random random = new Random();

            int[,] tabla = new int[lado, lado];
            for (int i = 0; i < lado; i++)
            {
                for (int j = 0; j < lado; j++)
                {
                    tabla[i, j] = random.Next(1, 9);

                }
            }
            ViewBag.tabla = tabla;
            ViewBag.lado = lado;
            return View("Global");
        }
        [Route("/magico/{lado}")]
        public IActionResult magico(int lado)
        {
        
            if (lado % 2 == 0)
            {
                return NotFound();
            }
            int[,] tabla = new int[lado, lado];
            int x = lado / 2;
            int y = lado - 1;
            for (int i = 0; i < lado * lado; i++)
            {
                if(tabla[y, x] != 0)
                {
                    x = (x - 1 + lado) % lado;
                    y = (y - 1 + lado) % lado;
                    while (tabla[y,x] != 0) {
                        y = (y - 1 + lado) % lado;
                    }
                }
               
                tabla[y,x] = i+1;
                x = (x + 1 + lado) % lado;
                y = (y + 1 + lado) % lado;
            }
            ViewBag.tabla = tabla;
            ViewBag.lado = lado;
            return View("Global");
        }
    }
}