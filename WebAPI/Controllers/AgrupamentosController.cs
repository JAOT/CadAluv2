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
    public class AgrupamentosController : ControllerBase
    {
        private readonly AgrupamentoContext _context;

        public AgrupamentosController(AgrupamentoContext context)
        {
            _context = context;
        }

        // GET: api/Agrupamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agrupamento>>> GetAgrupamentos()
        {
            return await _context.Agrupamentos.ToListAsync();
        }

        // GET: api/Agrupamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agrupamento>> GetAgrupamento(int id)
        {
            var agrupamento = await _context.Agrupamentos.FindAsync(id);

            if (agrupamento == null)
            {
                return NotFound();
            }

            return agrupamento;
        }

        // PUT: api/Agrupamentos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgrupamento(int id, Agrupamento agrupamento)
        {
            if (id != agrupamento.id)
            {
                return BadRequest();
            }

            _context.Entry(agrupamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgrupamentoExists(id))
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

        // POST: api/Agrupamentos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Agrupamento>> PostAgrupamento(Agrupamento agrupamento)
        {
            _context.Agrupamentos.Add(agrupamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgrupamento", new { id = agrupamento.id }, agrupamento);
        }

        // DELETE: api/Agrupamentos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Agrupamento>> DeleteAgrupamento(int id)
        {
            var agrupamento = await _context.Agrupamentos.FindAsync(id);
            if (agrupamento == null)
            {
                return NotFound();
            }

            _context.Agrupamentos.Remove(agrupamento);
            await _context.SaveChangesAsync();

            return agrupamento;
        }

        private bool AgrupamentoExists(int id)
        {
            return _context.Agrupamentos.Any(e => e.id == id);
        }
    }
}
