using VerdantisModel;
using Microsoft.EntityFrameworkCore;
using VerdantisData.Configurations;

namespace VerdantisData;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ProdutorModel> Produtores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProdutorConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
