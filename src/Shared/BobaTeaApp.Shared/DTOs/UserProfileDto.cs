namespace BobaTeaApp.Shared.DTOs;

public sealed record UserProfileDto(
    Guid Id,
    string Email,
    string FullName,
    string? AvatarUrl,
    string? PhoneNumber,
    IReadOnlyList<PaymentMethodDto> PaymentMethods,
    IReadOnlyList<Guid> FavoriteProductIds);
