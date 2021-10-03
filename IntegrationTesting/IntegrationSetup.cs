using EventManagementSystem.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTesting
{
  [SetUpFixture]
  public class IntegrationSetup
  {
    [OneTimeSetUp]
    public async Task SetUpDatabase()
    {
      var dbContextFactory = new ApplicationDbContextFactory();
      using (var dbContext = dbContextFactory.CreateDbContext(new string[] { }))
      {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
      }
    }
  }
}
