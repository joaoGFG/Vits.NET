using VerdantisModel;

namespace VerdantisBusiness;

public interface IProdutorService
{
    List<ProdutorModel> ListarTodos();
    ProdutorModel? ObterPorId(string id);
    ProdutorModel Criar(ProdutorModel produtor);
    bool Atualizar(ProdutorModel produtor);
    bool Remover(string id);
}
