using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPortal.Data;
using WebPortal.Models;

namespace WebPortal.Views
{
    public class AgrupamentosController : Controller
    {
        private readonly WebPortalContext _context;

        public AgrupamentosController(WebPortalContext context)
        {
            _context = context;
        }

        // GET: Agrupamentos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agrupamentos.ToListAsync());
        }

        // GET: Agrupamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agrupamento = await _context.Agrupamentos
                .FirstOrDefaultAsync(m => m.id == id);
            if (agrupamento == null)
            {
                return NotFound();
            }

            return View(agrupamento);
        }

        // GET: Agrupamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agrupamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Nome")] Agrupamento agrupamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agrupamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agrupamento);
        }

        // GET: Agrupamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agrupamento = await _context.Agrupamentos.FindAsync(id);
            if (agrupamento == null)
            {
                return NotFound();
            }
            return View(agrupamento);
        }

        // POST: Agrupamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Nome")] Agrupamento agrupamento)
        {
            if (id != agrupamento.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agrupamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgrupamentoExists(agrupamento.id))
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
            return View(agrupamento);
        }

        // GET: Agrupamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agrupamento = await _context.Agrupamentos
                .FirstOrDefaultAsync(m => m.id == id);
            if (agrupamento == null)
            {
                return NotFound();
            }

            return View(agrupamento);
        }

        // POST: Agrupamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agrupamento = await _context.Agrupamentos.FindAsync(id);
            _context.Agrupamentos.Remove(agrupamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgrupamentoExists(int id)
        {
            return _context.Agrupamentos.Any(e => e.id == id);
        }
    }
}
