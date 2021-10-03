using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementSystem.Models
{
  public class EventGuest
  {
    public int Id { get; set; }
    public int EventId { get; set; }
    public Event Event { get; set; }
    public int GuestId { get; set; }
    public Guest Guest { get; set; }
  }
}
