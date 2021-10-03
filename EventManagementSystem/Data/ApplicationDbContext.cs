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
    public DbSet<Event> Events { get; set; }
    public DbSet<EventGuest> EventGuests { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Unique Email Property
      modelBuilder.Entity<Guest>()
        .HasIndex(x => x.Email)
        .IsUnique();

      // Guest and Allergy RelationShip
      modelBuilder.Entity<GuestAllergy>()
       .HasKey(ga => new { ga.GuestId, ga.AllergyId });
      modelBuilder.Entity<GuestAllergy>()
        .HasOne(ga => ga.Guest)
        .WithMany(g => g.GuestAllergies)
        .HasForeignKey(ga => ga.GuestId);
      modelBuilder.Entity<GuestAllergy>()
        .HasOne(ga => ga.Allergy)
        .WithMany(a => a.GuestAllergies)
        .HasForeignKey(ga => ga.AllergyId);
    }
  }
}
