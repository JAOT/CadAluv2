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
    public class PaisController : ControllerBase
    {
        private readonly PaiContext _context;

        public PaisController(PaiContext context)
        {
            _context = context;
        }

        // GET: api/Pais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pai>>> GetPais()
        {
            return await _context.Pais.ToListAsync();
        }

        // GET: api/Pais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pai>> GetPai(int id)
        {
            var pai = await _context.Pais.FindAsync(id);

            if (pai == null)
            {
                return NotFound();
            }

            return pai;
        }

        // PUT: api/Pais/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPai(int id, Pai pai)
        {
            if (id != pai.id)
            {
                return BadRequest();
            }

            _context.Entry(pai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaiExists(id))
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

        // POST: api/Pais
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pai>> PostPai(Pai pai)
        {
            _context.Pais.Add(pai);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPai", new { id = pai.id }, pai);
        }

        // DELETE: api/Pais/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pai>> DeletePai(int id)
        {
            var pai = await _context.Pais.FindAsync(id);
            if (pai == null)
            {
                return NotFound();
            }

            _context.Pais.Remove(pai);
            await _context.SaveChangesAsync();

            return pai;
        }

        private bool PaiExists(int id)
        {
            return _context.Pais.Any(e => e.id == id);
        }
    }
}
