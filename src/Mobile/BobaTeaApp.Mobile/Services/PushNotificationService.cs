namespace BobaTeaApp.Mobile.Services;

public sealed class PushNotificationService
{
    public Task InitializeAsync()
    {
        // Wire up Firebase / native notification providers in platform projects.
        return Task.CompletedTask;
    }

    public Task RegisterTokenAsync(string token)
    {
        // Send device token to backend for push notifications.
        return Task.CompletedTask;
    }
}
