namespace BobaTeaApp.Shared.DTOs;

public sealed record ProductDto(
    Guid Id,
    Guid CategoryId,
    string Name,
    string? Description,
    decimal BasePrice,
    string? ImageUrl,
    bool IsAvailable,
    bool IsFeatured,
    decimal? Calories,
    IReadOnlyList<ProductOptionDto> Options);

public sealed record ProductOptionDto(
    Guid Id,
    string GroupName,
    string Label,
    decimal AdditionalPrice,
    bool IsDefault);
