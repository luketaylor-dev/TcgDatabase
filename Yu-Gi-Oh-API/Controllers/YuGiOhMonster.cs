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
    public class YuGiOhMonster : ControllerBase
    {
        private readonly AppDbContext _context;

        public YuGiOhMonster(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/YuGiOhMonster
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonsterCardModel>>> GetMonsterCards()
        {
          if (_context.MonsterCards == null)
          {
              return NotFound();
          }
            return await _context.MonsterCards.ToListAsync();
        }

        // GET: api/YuGiOhMonster/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MonsterCardModel>> GetMonsterCardModel(int id)
        {
          if (_context.MonsterCards == null)
          {
              return NotFound();
          }
            var monsterCardModel = await _context.MonsterCards.FindAsync(id);

            if (monsterCardModel == null)
            {
                return NotFound();
            }

            return monsterCardModel;
        }

        // PUT: api/YuGiOhMonster/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonsterCardModel(int id, MonsterCardModel monsterCardModel)
        {
            if (id != monsterCardModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(monsterCardModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonsterCardModelExists(id))
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

        // POST: api/YuGiOhMonster
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MonsterCardModel>> PostMonsterCardModel(MonsterCardModel monsterCardModel)
        {
          if (_context.MonsterCards == null)
          {
              return Problem("Entity set 'AppDbContext.MonsterCards'  is null.");
          }
            _context.MonsterCards.Add(monsterCardModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMonsterCardModel", new { id = monsterCardModel.Id }, monsterCardModel);
        }

        // DELETE: api/YuGiOhMonster/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonsterCardModel(int id)
        {
            if (_context.MonsterCards == null)
            {
                return NotFound();
            }
            var monsterCardModel = await _context.MonsterCards.FindAsync(id);
            if (monsterCardModel == null)
            {
                return NotFound();
            }

            _context.MonsterCards.Remove(monsterCardModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MonsterCardModelExists(int id)
        {
            return (_context.MonsterCards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
