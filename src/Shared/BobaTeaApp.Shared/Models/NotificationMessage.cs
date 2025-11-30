using BobaTeaApp.Shared.Enums;

namespace BobaTeaApp.Shared.Models;

public sealed class NotificationMessage : BaseEntity
{
    public required Guid UserId { get; set; }
    public required NotificationChannel Channel { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
    public bool IsRead { get; set; }
    public DateTimeOffset SentAt { get; set; } = DateTimeOffset.UtcNow;
}
