namespace BobaTeaApp.Shared.DTOs;

public sealed record PaymentMethodDto(
    Guid Id,
    string Provider,
    string MaskedCardNumber,
    int ExpMonth,
    int ExpYear,
    bool IsDefault);
