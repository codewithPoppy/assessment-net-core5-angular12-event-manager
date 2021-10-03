using EventManagementSystem.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTesting
{
  [TestFixture]
  public class GuestTests : IntegrationTestBase
  {
    [Test]
    public async Task AddGuest()
    {
      Guest guest = new Guest();
      guest.FirstName = "John";
      guest.LastName = "Doe";
      guest.Email = "john@test.com";
      guest.DateOfBirth = new DateTime();

      _dbContext.Guests.Add(guest);
      await _dbContext.SaveChangesAsync();

      var guestFromDb = _dbContext.Guests.Find(guest.Id);
      Assert.AreEqual(guestFromDb.FirstName, guest.FirstName);
      Assert.AreEqual(guestFromDb.LastName, guest.LastName);
      Assert.AreEqual(guestFromDb.Email, guest.Email);
      Assert.AreEqual(guestFromDb.DateOfBirth, guest.DateOfBirth);
    }
  }
}
