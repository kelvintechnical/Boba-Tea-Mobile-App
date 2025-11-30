namespace BobaTeaApp.Api.Entities;

public sealed class TaxRateEntity
{
    public Guid Id { get; set; }
    public required string Country { get; set; }
    public required string Region { get; set; }
    public decimal Rate { get; set; }
}
