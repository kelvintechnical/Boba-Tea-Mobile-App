namespace BobaTeaApp.Shared.Models;

public sealed class UserFavorite : BaseEntity
{
    public required Guid UserId { get; set; }
    public required Guid ProductId { get; set; }
    public DateTimeOffset FavoritedAt { get; set; } = DateTimeOffset.UtcNow;
}
