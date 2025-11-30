namespace BobaTeaApp.Api.Entities;

public sealed class ProductOptionEntity
{
    public Guid Id { get; set; }
    public required Guid ProductId { get; set; }
    public required string GroupName { get; set; }
    public required string Label { get; set; }
    public decimal AdditionalPrice { get; set; }
    public bool IsDefault { get; set; }

    public ProductEntity? Product { get; set; }
}
