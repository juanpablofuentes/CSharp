using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FirsSteps
{
    class Alumno
    {
        [Key]
        public int idAlumno { get; set; }
        public string Nombre { get; set; }
    }
}
