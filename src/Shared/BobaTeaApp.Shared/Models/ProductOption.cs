namespace BobaTeaApp.Shared.Models;

public sealed class ProductOption : BaseEntity
{
    public required Guid ProductId { get; set; }
    public required string GroupName { get; set; }
    public required string Label { get; set; }
    public decimal AdditionalPrice { get; set; }
    public bool IsDefault { get; set; }
}
