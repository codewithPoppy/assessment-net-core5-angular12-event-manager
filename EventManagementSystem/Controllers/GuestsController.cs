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
  public class GuestsController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public GuestsController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: api/Guests
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Guest>>> GetGuests()
    {
      return await _context.Guests.Include(g => g.Allergies).ToListAsync();
    }

    // GET: api/Guests/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Guest>> GetGuest(int id)
    {
      var guest = await _context.Guests.Include(g => g.Allergies).SingleAsync(g => g.Id == id);

      if (guest == null)
      {
        return NotFound();
      }

      return guest;
    }

    // PUT: api/Guests/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGuest(int id, Guest guest)
    {
      if (id != guest.Id)
      {
        return BadRequest();
      }

      _context.Entry(guest).State = EntityState.Modified;

      try
      {
        Guest guestOrg = await _context.Guests.Include(g => g.Allergies).SingleAsync(g => g.Id == id);
        List<Allergy> allergyList = new List<Allergy>();
        foreach (Allergy allergy in guest.Allergies)
        {
          if (allergy.Id == 0)
            allergyList.Add(new Allergy(allergy.Name));
          else
            allergyList.Add(_context.Allergies.Find(allergy.Id));
        }
        guest.Allergies = allergyList;
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!GuestExists(id))
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

    // POST: api/Guests
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<Guest>> PostGuest(Guest guest, [FromForm] Allergy[] allergies)
    {
      List<Allergy> allergyList = new List<Allergy>();
      foreach (Allergy allergy in guest.Allergies)
      {
        if (allergy.Id == 0)
          allergyList.Add(new Allergy(allergy.Name));
        else
          allergyList.Add(_context.Allergies.Find(allergy.Id));
      }
      guest.Allergies = allergyList;
      _context.Guests.Add(guest);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetGuest", new
      {
        id = guest.Id
      }, guest);
    }

    // DELETE: api/Guests/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Guest>> DeleteGuest(int id)
    {
      var guest = await _context.Guests.FindAsync(id);
      if (guest == null)
      {
        return NotFound();
      }

      _context.Guests.Remove(guest);
      await _context.SaveChangesAsync();

      return guest;
    }

    private bool GuestExists(int id)
    {
      return _context.Guests.Any(e => e.Id == id);
    }
  }
}
