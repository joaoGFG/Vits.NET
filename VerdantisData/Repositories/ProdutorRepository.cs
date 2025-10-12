using Microsoft.EntityFrameworkCore;
using VerdantisBusiness;
using VerdantisModel;

namespace VerdantisData;

public class ProdutorRepository(ApplicationDbContext context) : IProdutorRepository
{
    private readonly ApplicationDbContext _context = context;

    public List<ProdutorModel> GetAll() => _context.Produtores.AsNoTracking().ToList();
    public ProdutorModel? GetById(string id) => _context.Produtores.Find(id);
    public void Add(ProdutorModel produtor) => _context.Produtores.Add(produtor);
    public void Update(ProdutorModel produtor) => _context.Produtores.Update(produtor);
    public void Remove(ProdutorModel produtor) => _context.Produtores.Remove(produtor);
    public int SaveChanges() => _context.SaveChanges();
}