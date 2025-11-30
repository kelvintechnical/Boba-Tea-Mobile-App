namespace BobaTeaApp.Shared.Models;

public sealed class Product : BaseEntity
{
    public required Guid CategoryId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsAvailable { get; set; } = true;
    public bool IsFeatured { get; set; }
    public decimal? Calories { get; set; }
    public string? Tags { get; set; }
}
