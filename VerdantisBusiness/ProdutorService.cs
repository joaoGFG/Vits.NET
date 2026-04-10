using VerdantisBusiness.DTOs;
using VerdantisModel;

namespace VerdantisBusiness;

public class ProdutorService(IProdutorRepository repo) : IProdutorService
{
    private readonly IProdutorRepository _repo = repo;

    public List<ProdutorResponseDto> ListarTodos()
        => _repo.GetAll().Select(MapToResponse).ToList();

    public ProdutorResponseDto? ObterPorId(int id)
        => _repo.GetById(id) is { } entity ? MapToResponse(entity) : null;

    public ProdutorResponseDto Criar(ProdutorCreateDto dto)
    {
        var entity = new ProdutorModel
        {
            Nome = dto.Nome.Trim(),
            TipoUsuarioId = dto.TipoUsuarioId,
            Senha = dto.Senha, 
            DataCadastro = DateTime.UtcNow
        };

        _repo.Add(entity);
        _repo.SaveChanges();

        return MapToResponse(entity);
    }

    public bool Atualizar(ProdutorUpdateDto dto)
    {
        var existente = _repo.GetById(dto.Id);
        if (existente == null) return false;

        existente.Nome = dto.Nome.Trim();
        existente.TipoUsuarioId = dto.TipoUsuarioId;

        _repo.Update(existente);
        _repo.SaveChanges();
        return true;
    }

    public bool Remover(int id)
    {
        var existente = _repo.GetById(id);
        if (existente == null) return false;

        _repo.Remove(existente);
        _repo.SaveChanges();
        return true;
    }

    public PagedResultDto<ProdutorResponseDto> Search(string? nome, int page, int size, string sortBy, bool ascending = true)
    {
        // Convertendo List para IEnumerable para usar LINQ
        var query = _repo.GetAll().AsEnumerable();

        // Filtro por nome
        if (!string.IsNullOrWhiteSpace(nome))
        {
            query = query.Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));
        }

        // Ordenação
        query = sortBy.ToLower() switch
        {
            "nome" => ascending ? query.OrderBy(p => p.Nome) : query.OrderByDescending(p => p.Nome),
            "datacadastro" => ascending ? query.OrderBy(p => p.DataCadastro) : query.OrderByDescending(p => p.DataCadastro),
            "tipousuarioid" => ascending ? query.OrderBy(p => p.TipoUsuarioId) : query.OrderByDescending(p => p.TipoUsuarioId),
            _ => ascending ? query.OrderBy(p => p.Nome) : query.OrderByDescending(p => p.Nome)
        };

        var totalItems = query.Count();
        var totalPages = (int)Math.Ceiling((double)totalItems / size);
        
        var items = query
            .Skip((page - 1) * size)
            .Take(size)
            .Select(MapToResponse)
            .ToList();

        return new PagedResultDto<ProdutorResponseDto>(
            items,
            totalItems,
            page,
            size,
            totalPages,
            page < totalPages,
            page > 1
        );
    }

    private static ProdutorResponseDto MapToResponse(ProdutorModel p) =>
        new(p.Id, p.Nome, p.DataCadastro, p.TipoUsuarioId);
}
