using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EventManagementSystem.Data
{
  public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
  {
    public ApplicationDbContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
      optionsBuilder.UseSqlServer(
        "Server=(localdb)\\mssqllocaldb;Database=EFIntegration.Testing;Trusted_Connection=True;");

      return new ApplicationDbContext(optionsBuilder.Options);
    }
  }
}
