using EventManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementSystem.Data
{
  public class ApplicationDbContext : DbContext
  {
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Allergy> Allergies { get; set; }
    public DbSet<Event> Events { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Unique Email Property
      modelBuilder.Entity<Guest>()
        .HasIndex(x => x.Email)
        .IsUnique();
      // Unique Allergy Property
      modelBuilder.Entity<Allergy>()
        .HasIndex(x => x.NormalizedName)
        .IsUnique();
    }
  }
}
