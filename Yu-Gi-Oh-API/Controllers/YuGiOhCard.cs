using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yu_Gi_Oh_Infrasturcture;
using Yu_Gi_Oh_Infrasturcture.Models;

namespace Yu_Gi_Oh_Database.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YuGiOhCard : ControllerBase
    {
        private readonly AppDbContext _context;

        public YuGiOhCard(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/YuGiOhCard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardModel>>> GetCards()
        {
          if (_context.Cards == null)
          {
              return NotFound();
          }
            return await _context.Cards.ToListAsync();
        }

        // GET: api/YuGiOhCard/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardModel>> GetCardModel(string id)
        {
          if (_context.Cards == null)
          {
              return NotFound();
          }
            var cardModel = await _context.Cards.FindAsync(id);

            if (cardModel == null)
            {
                return NotFound();
            }

            return cardModel;
        }

        // PUT: api/YuGiOhCard/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardModel(string id, CardModel cardModel)
        {
            if (id != cardModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(cardModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardModelExists(id))
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

        // POST: api/YuGiOhCard
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CardModel>> PostCardModel(CardModel cardModel)
        {
          if (_context.Cards == null)
          {
              return Problem("Entity set 'AppDbContext.Cards'  is null.");
          }
            _context.Cards.Add(cardModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CardModelExists(cardModel.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCardModel", new { id = cardModel.Id }, cardModel);
        }

        // DELETE: api/YuGiOhCard/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardModel(string id)
        {
            if (_context.Cards == null)
            {
                return NotFound();
            }
            var cardModel = await _context.Cards.FindAsync(id);
            if (cardModel == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(cardModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardModelExists(string id)
        {
            return (_context.Cards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
