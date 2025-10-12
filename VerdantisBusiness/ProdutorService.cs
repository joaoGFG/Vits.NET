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

    private static ProdutorResponseDto MapToResponse(ProdutorModel p) =>
        new(p.Id, p.Nome, p.DataCadastro, p.TipoUsuarioId);
}
