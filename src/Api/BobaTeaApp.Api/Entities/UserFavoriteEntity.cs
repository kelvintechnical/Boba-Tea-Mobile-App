namespace BobaTeaApp.Api.Entities;

public sealed class UserFavoriteEntity
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required Guid ProductId { get; set; }
    public DateTimeOffset FavoritedAt { get; set; } = DateTimeOffset.UtcNow;

    public ApplicationUser? User { get; set; }
    public ProductEntity? Product { get; set; }
}
