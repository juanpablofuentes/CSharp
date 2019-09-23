﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCLite.Models;

namespace MVCLite.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly Contexto _context;

        public AlumnoController(Contexto context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IList<Alumno> alumnos = new List<Alumno>();

            alumnos.Add(new Alumno { Nombre = "Eva", Nota = 5 });
            alumnos.Add(new Alumno { Nombre = "Ana", Nota = 7 });
            alumnos.Add(new Alumno { Nombre = "Ot", Nota = 9 });
          //  Contexto c = new Contexto();
            return View(_context.Alumnos);
        }
        public IActionResult Add()
        {
            return View();
        }
    }
}