using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinasController : ControllerBase
    {
        private readonly DisciplinaContext _context;

        public DisciplinasController(DisciplinaContext context)
        {
            _context = context;
        }

        // GET: api/Disciplinas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disciplina>>> GetDisciplinas()
        {
            return await _context.Disciplinas.ToListAsync();
        }

        // GET: api/Disciplinas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Disciplina>> GetDisciplina(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);

            if (disciplina == null)
            {
                return NotFound();
            }

            return disciplina;
        }

        // PUT: api/Disciplinas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisciplina(int id, Disciplina disciplina)
        {
            if (id != disciplina.id)
            {
                return BadRequest();
            }

            _context.Entry(disciplina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisciplinaExists(id))
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

        // POST: api/Disciplinas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Disciplina>> PostDisciplina(Disciplina disciplina)
        {
            _context.Disciplinas.Add(disciplina);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDisciplina", new { id = disciplina.id }, disciplina);
        }

        // DELETE: api/Disciplinas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Disciplina>> DeleteDisciplina(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);
            if (disciplina == null)
            {
                return NotFound();
            }

            _context.Disciplinas.Remove(disciplina);
            await _context.SaveChangesAsync();

            return disciplina;
        }

        private bool DisciplinaExists(int id)
        {
            return _context.Disciplinas.Any(e => e.id == id);
        }
    }
}
