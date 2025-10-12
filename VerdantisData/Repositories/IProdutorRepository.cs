using VerdantisModel;

namespace VerdantisBusiness;

public interface IProdutorRepository
{
    List<ProdutorModel> GetAll();
    ProdutorModel? GetById(string id);
    void Add(ProdutorModel produtor);
    void Update(ProdutorModel produtor);
    void Remove(ProdutorModel produtor);
    int SaveChanges();
}