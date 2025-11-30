using BobaTeaApp.Mobile.Services;
using BobaTeaApp.Shared.Requests;
using CommunityToolkit.Mvvm.Input;

namespace BobaTeaApp.Mobile.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    private readonly AuthService _authService;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _fullName = string.Empty;

    [ObservableProperty]
    private string _phoneNumber = string.Empty;

    public RegisterViewModel(AuthService authService)
    {
        _authService = authService;
    }

    [RelayCommand]
    private Task RegisterAsync() => ExecuteSafeAsync(async () =>
    {
        await _authService.RegisterAsync(new RegisterRequest(Email, Password, FullName, PhoneNumber));
    });
}
