using BobaTeaApp.Shared.DTOs;

namespace BobaTeaApp.Api.Services;

public sealed class NotificationService
{
    public Task SendOrderPlacedAsync(Guid userId, OrderDetailDto order)
    {
        // Integrate with Azure Notification Hubs, Firebase, or SendGrid here.
        return Task.CompletedTask;
    }

    public Task SendStatusUpdateAsync(Guid userId, string status)
    {
        return Task.CompletedTask;
    }
}
