using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VerdantisData;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseOracle("User Id=system;Password=dOJN@IhD12342;Data Source=localhost:1521/FREEPDB1;");
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
