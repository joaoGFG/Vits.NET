using VerdantisModel;
using Microsoft.EntityFrameworkCore;

namespace VerdantisData;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ProdutorModel> Produtores { get; set; }
}
