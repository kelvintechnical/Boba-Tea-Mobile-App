namespace BobaTeaApp.Shared.Models;

public sealed class PaymentMethod : BaseEntity
{
    public required Guid UserId { get; set; }
    public required string Provider { get; set; }
    public required string MaskedCardNumber { get; set; }
    public required string Token { get; set; }
    public int ExpMonth { get; set; }
    public int ExpYear { get; set; }
    public bool IsDefault { get; set; }
}
