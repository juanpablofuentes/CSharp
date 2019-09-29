using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCComics;

namespace MVCComics.Controllers
{
    public class ComicsController : Controller
    {
        private readonly Contexto _context;

        public ComicsController(Contexto context)
        {
            _context = context;
        }

        // GET: Comics
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Comic.Include(c => c.Categoria);
            return View(await contexto.ToListAsync());
        }

        // GET: Comics/Detalles/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comic = await _context.Comic
                .Include(c => c.Categoria)
                .Include(c=>c.ComicAutor)
                .ThenInclude(ca=>ca.Autor)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewData["AutorId"] = new SelectList(_context.Autor, "Id", "Nombre");

            if (comic == null)
            {
                return NotFound();
            }

            return View(comic);
        }

        // GET: Comics/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nombre");
            return View();
        }

        // POST: Comics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more Detalles see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,Fecha,CategoriaId")] Comic comic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nombre", comic.CategoriaId);
            return View(comic);
        }

        // POST: Autors/AddComic/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAutor(int id, [Bind("AutorId,Rol")] ComicAutor comicautor)
        {
            comicautor.ComicId = id;


            try
            {
                _context.ComicAutor.Add(comicautor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }
            return RedirectToAction("Details", new { Id = id });

        }
        // GET: Comics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comic = await _context.Comic.FindAsync(id);
            if (comic == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nombre", comic.CategoriaId);
            return View(comic);
        }

        // POST: Comics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more Detalles see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,Fecha,CategoriaId")] Comic comic)
        {
            if (id != comic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComicExists(comic.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Id", comic.CategoriaId);
            return View(comic);
        }

        // GET: Comics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comic = await _context.Comic
                .Include(c => c.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comic == null)
            {
                return NotFound();
            }

            return View(comic);
        }

        // POST: Comics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comic = await _context.Comic.FindAsync(id);
            _context.Comic.Remove(comic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComicExists(int id)
        {
            return _context.Comic.Any(e => e.Id == id);
        }
    }
}
