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
  public class EventsController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public EventsController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: api/Events
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
    {
      return await _context.Events.Include(e => e.Guests).ToListAsync();
    }

    // GET: api/Events/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEvent(int id)
    {
      var @event = await _context.Events.Include(e => e.Guests).SingleAsync(e => e.Id == id);

      if (@event == null)
      {
        return NotFound();
      }

      return @event;
    }

    // PUT: api/Events/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEvent(int id, Event @event)
    {
      if (id != @event.Id)
      {
        return BadRequest();
      }

      _context.Entry(@event).State = EntityState.Modified;

      try
      {
        Event eventOrg = await _context.Events.Include(e => e.Guests).SingleAsync(e => e.Id == id);
        List<Guest> guestList = new List<Guest>();
        foreach (Guest guest in @event.Guests)
        {
          guestList.Add(_context.Guests.Find(guest.Id));
        }
        @event.Guests = guestList;
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!EventExists(id))
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

    // POST: api/Events
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<Event>> PostEvent(Event @event)
    {
      List<Guest> guestList = new List<Guest>();
      foreach (var guest in @event.Guests)
      {
        guestList.Add(_context.Guests.Find(guest.Id));
      }
      @event.Guests = guestList;
      _context.Events.Add(@event);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
    }

    // DELETE: api/Events/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Event>> DeleteEvent(int id)
    {
      var @event = await _context.Events.FindAsync(id);
      if (@event == null)
      {
        return NotFound();
      }

      _context.Events.Remove(@event);
      await _context.SaveChangesAsync();

      return @event;
    }

    private bool EventExists(int id)
    {
      return _context.Events.Any(e => e.Id == id);
    }
  }
}
