namespace BobaTeaApp.Shared.Models;

public sealed class ProductCategory : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? HeroImageUrl { get; set; }
    public int DisplayOrder { get; set; }
}
