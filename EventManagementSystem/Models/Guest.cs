using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementSystem.Models
{
  public class Guest
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public virtual string FullName => FirstName + " " + LastName;

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public ICollection<GuestAllergy> GuestAllergies { get; set; }

    public ICollection<EventGuest> EventGuests { get; set; }
  }

  public class Allergy
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<GuestAllergy> GuestAllergies { get; set; }
  }

  public class GuestAllergy
  {
    public int GuestId { get; set; }
    public Guest Guest { get; set; }
    public int AllergyId { get; set; }
    public Allergy Allergy { get; set; }
  }
}
