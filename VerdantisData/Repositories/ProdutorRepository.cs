using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using VerdantisBusiness;
using VerdantisModel;

namespace VerdantisData;

public class ProdutorRepository(ApplicationDbContext context) : IProdutorRepository
{
    private readonly ApplicationDbContext _context = context;

    public List<ProdutorModel> GetAll() => _context.Produtores.AsNoTracking().ToList();

    public ProdutorModel? GetById(int id) => _context.Produtores.Find(id);

    public void Add(ProdutorModel produtor)
    {
        var conn = _context.Database.GetDbConnection();
        var closeAtEnd = false;
        if (conn.State != System.Data.ConnectionState.Open)
        {
            conn.Open();
            closeAtEnd = true;
        }

        try
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT SEQ_USUARIO.NEXTVAL FROM DUAL";
            var result = cmd.ExecuteScalar();
            produtor.Id = Convert.ToInt32(result);

            _context.Produtores.Add(produtor);
        }
        finally
        {
            if (closeAtEnd) conn.Close();
        }
    }

    public void Update(ProdutorModel produtor) => _context.Produtores.Update(produtor);

    public void Remove(ProdutorModel produtor) => _context.Produtores.Remove(produtor);

    public int SaveChanges() => _context.SaveChanges();
}