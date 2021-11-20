using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPortalCadAlu.Data;
using WebPortalCadAlu.Models;

namespace WebPortalCadAlu.Controllers
{
    public class AlunosController : Controller
    {
        private readonly cadaluContext _context;

        public AlunosController(cadaluContext context)
        {
            _context = context;
        }

        // GET: Alunos
        public async Task<IActionResult> Index()
        {
            var cadaluContext = _context.Alunos.Include(a => a.Pai1Navigation).Include(a => a.Pai2Navigation).Include(a => a.TurmaNavigation);
            return View(await cadaluContext.ToListAsync());
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alunos = await _context.Alunos
                .Include(a => a.Pai1Navigation)
                .Include(a => a.Pai2Navigation)
                .Include(a => a.TurmaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alunos == null)
            {
                return NotFound();
            }

            return View(alunos);
        }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            ViewData["Pai1"] = new SelectList(_context.Pais, "Id", "Email");
            ViewData["Pai2"] = new SelectList(_context.Pais, "Id", "Email");
            ViewData["Turma"] = new SelectList(_context.Turmas, "Id", "Nome");
            return View();
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Turma,Pai1,Pai2")] Alunos alunos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alunos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Pai1"] = new SelectList(_context.Pais, "Id", "Email", alunos.Pai1);
            ViewData["Pai2"] = new SelectList(_context.Pais, "Id", "Email", alunos.Pai2);
            ViewData["Turma"] = new SelectList(_context.Turmas, "Id", "Nome", alunos.Turma);
            return View(alunos);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alunos = await _context.Alunos.FindAsync(id);
            if (alunos == null)
            {
                return NotFound();
            }
            ViewData["Pai1"] = new SelectList(_context.Pais, "Id", "Email", alunos.Pai1);
            ViewData["Pai2"] = new SelectList(_context.Pais, "Id", "Email", alunos.Pai2);
            ViewData["Turma"] = new SelectList(_context.Turmas, "Id", "Nome", alunos.Turma);
            return View(alunos);
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Turma,Pai1,Pai2")] Alunos alunos)
        {
            if (id != alunos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alunos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunosExists(alunos.Id))
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
            ViewData["Pai1"] = new SelectList(_context.Pais, "Id", "Email", alunos.Pai1);
            ViewData["Pai2"] = new SelectList(_context.Pais, "Id", "Email", alunos.Pai2);
            ViewData["Turma"] = new SelectList(_context.Turmas, "Id", "Nome", alunos.Turma);
            return View(alunos);
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alunos = await _context.Alunos
                .Include(a => a.Pai1Navigation)
                .Include(a => a.Pai2Navigation)
                .Include(a => a.TurmaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alunos == null)
            {
                return NotFound();
            }

            return View(alunos);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alunos = await _context.Alunos.FindAsync(id);
            _context.Alunos.Remove(alunos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunosExists(int id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }
    }
}
