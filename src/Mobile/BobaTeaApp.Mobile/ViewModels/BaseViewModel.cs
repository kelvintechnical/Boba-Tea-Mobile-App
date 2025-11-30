using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BobaTeaApp.Mobile.Services;

namespace BobaTeaApp.Mobile.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    protected readonly IToastService? ToastService;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private string _loadingText = "Loading...";

    public BaseViewModel(IToastService? toastService = null)
    {
        ToastService = toastService;
    }

    protected async Task ExecuteSafeAsync(
        Func<Task> action,
        string? loadingMessage = null,
        string? successMessage = null,
        bool showErrorToast = true,
        Action? before = null,
        Action? after = null)
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            ErrorMessage = null;
            LoadingText = loadingMessage ?? "Loading...";
            before?.Invoke();

            await action();

            if (!string.IsNullOrEmpty(successMessage) && ToastService != null)
            {
                await ToastService.ShowSuccessAsync(successMessage);
            }
        }
        catch (HttpRequestException ex)
        {
            var message = "Network error. Please check your connection.";
            ErrorMessage = message;
            if (showErrorToast && ToastService != null)
            {
                await ToastService.ShowErrorAsync(message);
            }
        }
        catch (TaskCanceledException)
        {
            var message = "Request timed out. Please try again.";
            ErrorMessage = message;
            if (showErrorToast && ToastService != null)
            {
                await ToastService.ShowErrorAsync(message);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = GetFriendlyErrorMessage(ex);
            if (showErrorToast && ToastService != null)
            {
                await ToastService.ShowErrorAsync(ErrorMessage);
            }
        }
        finally
        {
            IsBusy = false;
            after?.Invoke();
        }
    }

    private string GetFriendlyErrorMessage(Exception ex)
    {
        return ex.Message switch
        {
            var msg when msg.Contains("401") || msg.Contains("Unauthorized") => "Please log in to continue.",
            var msg when msg.Contains("403") || msg.Contains("Forbidden") => "You don't have permission to do that.",
            var msg when msg.Contains("404") || msg.Contains("Not Found") => "The requested item wasn't found.",
            var msg when msg.Contains("500") => "Server error. Please try again later.",
            _ => ex.Message
        };
    }
}
