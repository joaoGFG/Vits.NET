using VerdantisBusiness.DTOs;

namespace VerdantisApi.DTOs;

public record LinkDto(string Href, string Rel, string Method);

public record ProdutorHateoasDto(
    int Id,
    string Nome,
    DateTime DataCadastro,
    int TipoUsuarioId,
    List<LinkDto> Links
) : ProdutorResponseDto(Id, Nome, DataCadastro, TipoUsuarioId);

public record PagedHateoasResultDto<T>(
    List<T> Items,
    int TotalItems,
    int Page,
    int Size,
    int TotalPages,
    bool HasNextPage,
    bool HasPreviousPage,
    List<LinkDto> Links
);