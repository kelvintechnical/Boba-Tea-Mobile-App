namespace BobaTeaApp.Api.Entities;

public sealed class ProductCategoryEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? HeroImageUrl { get; set; }
    public int DisplayOrder { get; set; }
    public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}
