namespace VerdantisBusiness.DTOs;

public record ProdutorCreateDto(
    string Nome,
    int TipoUsuarioId,
    string Senha
);

public record ProdutorUpdateDto(
    int Id,
    string Nome,
    int TipoUsuarioId
);

public record ProdutorResponseDto(
    int Id,
    string Nome,
    DateTime DataCadastro,
    int TipoUsuarioId
);