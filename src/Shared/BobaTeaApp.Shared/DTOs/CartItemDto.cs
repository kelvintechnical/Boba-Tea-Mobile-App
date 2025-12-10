namespace BobaTeaApp.Shared.DTOs;

public sealed record CartItemDto(
    Guid ProductId,
    string Name,
    decimal UnitPrice,
    int Quantity,
    string? SelectedSize,
    string? SweetnessLevel,
    string? IceLevel,
    string? SelectedOptions,
    decimal LineTotal,
    IReadOnlyDictionary<string, string>? Customizations);
