using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace BobaTeaApp.Mobile.Services;

public interface IToastService
{
    Task ShowSuccessAsync(string message, ToastDuration duration = ToastDuration.Short);
    Task ShowErrorAsync(string message, ToastDuration duration = ToastDuration.Long);
    Task ShowInfoAsync(string message, ToastDuration duration = ToastDuration.Short);
}

public class ToastService : IToastService
{
    public async Task ShowSuccessAsync(string message, ToastDuration duration = ToastDuration.Short)
    {
        var toast = Toast.Make(message, duration, 14);
        await toast.Show();
    }

    public async Task ShowErrorAsync(string message, ToastDuration duration = ToastDuration.Long)
    {
        var toast = Toast.Make(message, duration, 14);
        await toast.Show();
    }

    public async Task ShowInfoAsync(string message, ToastDuration duration = ToastDuration.Short)
    {
        var toast = Toast.Make(message, duration, 14);
        await toast.Show();
    }
}
