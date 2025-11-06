using VerdantisBusiness.DTOs;

namespace VerdantisBusiness;

public interface IProdutorService
{
    List<ProdutorResponseDto> ListarTodos();
    ProdutorResponseDto? ObterPorId(int id);
    ProdutorResponseDto Criar(ProdutorCreateDto produtor);
    bool Atualizar(ProdutorUpdateDto produtor);
    bool Remover(int id);  
    PagedResultDto<ProdutorResponseDto> Search(string? nome, int page, int size, string sortBy, bool ascending = true);
}
