using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBasic.Models;

namespace MVCBasic.Controllers
{
    public class CientificoesController : Controller
    {
        private readonly MVCBasicContext _context;

        public CientificoesController(MVCBasicContext context)
        {
            _context = context;
        }

        // GET: Cientificoes
        public async Task<IActionResult> Index(string buscar)
        {

            var cientificos = from m in _context.Cientifico
                         select m;

            if (!String.IsNullOrEmpty(buscar))
            {
                cientificos = _context.Cientifico.Where(s => s.Nombre.Contains(buscar));
            }
            return View(await cientificos.ToListAsync());
        }

        // GET: Cientificoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cientifico = await _context.Cientifico
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cientifico == null)
            {
                return NotFound();
            }

            return View(cientifico);
        }

        // GET: Cientificoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cientificoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Nacionalidad,YearBirth")] Cientifico cientifico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cientifico);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(cientifico);
        }

        // GET: Cientificoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cientifico = await _context.Cientifico.FindAsync(id);
            if (cientifico == null)
            {
                return NotFound();
            }
            return View(cientifico);
        }

        // POST: Cientificoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Nacionalidad,YearBirth")] Cientifico cientifico)
        {
            if (id != cientifico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cientifico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CientificoExists(cientifico.Id))
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
            return View(cientifico);
        }

        // GET: Cientificoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cientifico = await _context.Cientifico
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cientifico == null)
            {
                return NotFound();
            }

            return View(cientifico);
        }

        // POST: Cientificoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cientifico = await _context.Cientifico.FindAsync(id);
            _context.Cientifico.Remove(cientifico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CientificoExists(int id)
        {
            return _context.Cientifico.Any(e => e.Id == id);
        }
    }
}
