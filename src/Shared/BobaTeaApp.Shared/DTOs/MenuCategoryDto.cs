namespace BobaTeaApp.Shared.DTOs;

public sealed record MenuCategoryDto(
    Guid Id,
    string Name,
    string? Description,
    string? HeroImageUrl,
    int DisplayOrder,
    IReadOnlyList<ProductDto> Products);
