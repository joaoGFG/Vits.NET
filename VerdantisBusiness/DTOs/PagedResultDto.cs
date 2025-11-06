namespace VerdantisBusiness.DTOs;

public record PagedResultDto<T>(
    List<T> Items,
    int TotalItems,
    int Page,
    int Size,
    int TotalPages,
    bool HasNextPage,
    bool HasPreviousPage
);