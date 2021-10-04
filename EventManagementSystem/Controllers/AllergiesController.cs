using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Data;
using EventManagementSystem.Models;

namespace EventManagementSystem.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AllergiesController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public AllergiesController(ApplicationDbContext context)
    {
      _context = context;

      if (!_context.Allergies.Any())
      {
        _context.Allergies.Add(new Allergy("Food"));
        _context.Allergies.Add(new Allergy("Animal"));
        _context.Allergies.Add(new Allergy("Drink"));
        _context.SaveChanges();
      }
    }

    // GET: api/Allergies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Allergy>>> GetAllergies()
    {
      return await _context.Allergies.ToListAsync();
    }

    // GET: api/Allergies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Allergy>> GetAllergy(int id)
    {
      var allergy = await _context.Allergies.FindAsync(id);

      if (allergy == null)
      {
        return NotFound();
      }

      return allergy;
    }

    // PUT: api/Allergies/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAllergy(int id, Allergy allergy)
    {
      if (id != allergy.Id)
      {
        return BadRequest();
      }

      _context.Entry(allergy).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!AllergyExists(id))
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

    // POST: api/Allergies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Allergy>> PostAllergy(Allergy allergy)
    {
      _context.Allergies.Add(allergy);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetAllergy", new { id = allergy.Id }, allergy);
    }

    // DELETE: api/Allergies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAllergy(int id)
    {
      var allergy = await _context.Allergies.FindAsync(id);
      if (allergy == null)
      {
        return NotFound();
      }

      _context.Allergies.Remove(allergy);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool AllergyExists(int id)
    {
      return _context.Allergies.Any(e => e.Id == id);
    }
  }
}
