using BobaTeaApp.Mobile.Services;
using BobaTeaApp.Shared.Requests;
using CommunityToolkit.Mvvm.Input;

namespace BobaTeaApp.Mobile.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly AuthService _authService;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    public LoginViewModel(AuthService authService)
    {
        _authService = authService;
    }

    [RelayCommand]
    private Task LoginAsync() => ExecuteSafeAsync(async () =>
    {
        await _authService.LoginAsync(new LoginRequest(Email, Password));
    });

    [RelayCommand]
    private Task NavigateRegisterAsync() =>
        Shell.Current.GoToAsync(nameof(Views.RegisterPage));
}
