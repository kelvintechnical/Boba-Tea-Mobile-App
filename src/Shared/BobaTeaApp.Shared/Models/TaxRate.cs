namespace BobaTeaApp.Shared.Models;

public sealed class TaxRate : BaseEntity
{
    public required string Country { get; set; }
    public required string Region { get; set; }
    public decimal Rate { get; set; }
}
