using VerdantisModel;
using VerdantisData;
using Microsoft.EntityFrameworkCore;

namespace VerdantisBusiness;

public class ProdutorService(ApplicationDbContext context) : IProdutorService
{
    private readonly ApplicationDbContext _context = context;

    public List<ProdutorModel> ListarTodos() => _context.Produtores.ToList();

    public ProdutorModel? ObterPorId(string id) =>
        _context.Produtores.FirstOrDefault(p => p.Id == id);

    public ProdutorModel Criar(ProdutorModel produtor)
    {
        _context.Produtores.Add(produtor);
        _context.SaveChanges();
        return produtor;
    }

    public bool Atualizar(ProdutorModel produtor)
    {
        var existente = _context.Produtores.Find(produtor.Id);
        if (existente == null) return false;

        existente.Nome = produtor.Nome;
        existente.Propriedade = produtor.Propriedade;
        existente.Localizacao = produtor.Localizacao;
        existente.TipoCultura = produtor.TipoCultura;
        existente.TamanhoHectares = produtor.TamanhoHectares;

        _context.SaveChanges();
        return true;
    }

    public bool Remover(string id)
    {
        var produtor = _context.Produtores.Find(id);
        if (produtor == null) return false;

        _context.Produtores.Remove(produtor);
        _context.SaveChanges();
        return true;
    }
}
