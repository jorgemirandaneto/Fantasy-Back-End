﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fantasy2.Context;
using Fantasy2.Models;

namespace Fantasy2.Controllers
{
    [Produces("application/json")]
    [Route("api/Etapas")]
    public class EtapasController : Controller
    {
        private readonly FantasyContext _context;

        public EtapasController(FantasyContext context)
        {
            _context = context;
        }

        // GET: api/Etapas
        [HttpGet]
        public IEnumerable<Etapa> GetEtapas()
        {
            return _context.Etapas;
        }

        // GET: api/Etapas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEtapa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var etapa = await _context.Etapas.SingleOrDefaultAsync(m => m.Id == id);

            if (etapa == null)
            {
                return NotFound();
            }

            return Ok(etapa);
        }

        // PUT: api/Etapas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEtapa([FromRoute] int id, [FromBody] Etapa etapa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != etapa.Id)
            {
                return BadRequest();
            }

            _context.Entry(etapa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtapaExists(id))
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

        // POST: api/Etapas
        [HttpPost]
        public async Task<IActionResult> PostEtapa([FromBody] Etapa etapa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Etapas.Add(etapa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEtapa", new { id = etapa.Id }, etapa);
        }

        // DELETE: api/Etapas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtapa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var etapa = await _context.Etapas.SingleOrDefaultAsync(m => m.Id == id);
            if (etapa == null)
            {
                return NotFound();
            }

            _context.Etapas.Remove(etapa);
            await _context.SaveChangesAsync();

            return Ok(etapa);
        }

        private bool EtapaExists(int id)
        {
            return _context.Etapas.Any(e => e.Id == id);
        }
    }
}