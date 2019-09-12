using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjemploController : ControllerBase
    {
        private readonly Contexto _context;

        public EjemploController(Contexto context)
        {
            _context = context;

            if (_context.Ejemplo.Count() == 0)
            {
                // Create a new Ejemplo if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Ejemplo.Add(new Ejemplo { Nombre = "Elemento1" });
                _context.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ejemplo>>> GetTodoItems()
        {
            return await _context.Ejemplo.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ejemplo>> GetTodoItem(long id)
        {
            var Ejemplo = await _context.Ejemplo.FindAsync(id);

            if (Ejemplo == null)
            {
                return NotFound();
            }

            return Ejemplo;
        }

        [HttpPost]
        public async Task<ActionResult<Ejemplo>> PostTodoItem(Ejemplo item)
        {
            _context.Ejemplo.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }
    }
}