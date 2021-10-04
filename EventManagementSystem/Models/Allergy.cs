using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementSystem.Models
{
  public class Allergy
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string NormalizedName { get; set; }

    public ICollection<Guest> Guests { get; set; }

    public Allergy(string name)
    {
      this.Name = name;
      this.NormalizedName = this.Name.Trim().ToUpper();
    }
  }
}
