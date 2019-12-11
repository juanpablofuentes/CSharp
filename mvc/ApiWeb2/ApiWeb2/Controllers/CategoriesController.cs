using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiWeb2.Models;
using ApiWeb2.DTO;

namespace ApiWeb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly Contexto _context;

        public CategoriesController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public IEnumerable<CategoryDTO> GetCategories()
        {
            var cats = from c in _context.Categories.Include(c=>c.Games)
                       select new CategoryDTO()
                       {
                           Id=c.Id,
                           Nombre = c.Nombre,
                           NumJuegos = c.Games.Count
        };
            return cats;
            //Otra manera old school
            IList<CategoryDTO> lista = new List<CategoryDTO>();
            foreach(var cat in _context.Categories.Include(c => c.Games))
            {
                lista.Add(new CategoryDTO() { Nombre = cat.Nombre, NumJuegos = cat.Games.Count });
            }
            return lista;
        }


        [HttpGet("[action]/{nombre}")]
        public IEnumerable<Category> Find(String nombre)
        {
            return _context.Categories.Where(c=>c.Nombre.Contains(nombre)).ToList();
        }
        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Categories.FindAsync(id);

            var games = await _context.Games.Where(g=>g.CategoryId == id).ToListAsync();
            if (category == null)
            {
                return NotFound();
            }

            CategoryDTO catDTO = new CategoryDTO {Id=category.Id,Nombre = category.Nombre, NumJuegos =games.Count };

            return Ok(catDTO);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory([FromRoute] int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}