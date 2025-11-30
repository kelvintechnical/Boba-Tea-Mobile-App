namespace BobaTeaApp.Api.Entities;

public sealed class PaymentMethodEntity
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required string Provider { get; set; }
    public required string MaskedCardNumber { get; set; }
    public required string Token { get; set; }
    public int ExpMonth { get; set; }
    public int ExpYear { get; set; }
    public bool IsDefault { get; set; }

    public ApplicationUser? User { get; set; }
}
