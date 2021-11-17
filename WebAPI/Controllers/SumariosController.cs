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
    public class SumariosController : ControllerBase
    {
        private readonly SumarioContext _context;

        public SumariosController(SumarioContext context)
        {
            _context = context;
        }

        // GET: api/Sumarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sumario>>> GetSumarios()
        {
            return await _context.Sumarios.ToListAsync();
        }

        // GET: api/Sumarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sumario>> GetSumario(int id)
        {
            var sumario = await _context.Sumarios.FindAsync(id);

            if (sumario == null)
            {
                return NotFound();
            }

            return sumario;
        }

        // PUT: api/Sumarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSumario(int id, Sumario sumario)
        {
            if (id != sumario.id)
            {
                return BadRequest();
            }

            _context.Entry(sumario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SumarioExists(id))
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

        // POST: api/Sumarios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Sumario>> PostSumario(Sumario sumario)
        {
            _context.Sumarios.Add(sumario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSumario", new { id = sumario.id }, sumario);
        }

        // DELETE: api/Sumarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sumario>> DeleteSumario(int id)
        {
            var sumario = await _context.Sumarios.FindAsync(id);
            if (sumario == null)
            {
                return NotFound();
            }

            _context.Sumarios.Remove(sumario);
            await _context.SaveChangesAsync();

            return sumario;
        }

        private bool SumarioExists(int id)
        {
            return _context.Sumarios.Any(e => e.id == id);
        }
    }
}
