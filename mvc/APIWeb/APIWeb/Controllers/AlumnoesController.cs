using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCLite.Models;

namespace APIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoesController : ControllerBase
    {
        private readonly Contexto _context;

        public AlumnoesController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Alumnoes
        [HttpGet]
        public IEnumerable<Alumno> GetAlumnos()
        {
            return _context.Alumnos;
        }

        // GET: api/Alumnoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlumno([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alumno = await _context.Alumnos.FindAsync(id);

            if (alumno == null)
            {
                return NotFound();
            }

            return Ok(alumno);
        }

        // PUT: api/Alumnoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlumno([FromRoute] int id, [FromBody] Alumno alumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var mialumno = await _context.Alumnos.FindAsync(id);
            if (mialumno == null)
            {
                return NotFound();
            }
            mialumno.Nombre = alumno.Nombre;
            mialumno.Nota = alumno.Nota;

            _context.Entry(mialumno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(mialumno);
        }

        // POST: api/Alumnoes
        [HttpPost]
        public async Task<IActionResult> PostAlumno([FromBody] Alumno alumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlumno", new { id = alumno.Id }, alumno);
        }

        // DELETE: api/Alumnoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlumno([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }

            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();

            return Ok(alumno);
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.Id == id);
        }
    }
}