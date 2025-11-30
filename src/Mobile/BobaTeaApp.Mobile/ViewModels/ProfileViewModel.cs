using BobaTeaApp.Mobile.Services;
using BobaTeaApp.Shared.DTOs;
using CommunityToolkit.Mvvm.Input;

namespace BobaTeaApp.Mobile.ViewModels;

public partial class ProfileViewModel : BaseViewModel
{
    private readonly ProfileService _profileService;
    private readonly AuthService _authService;

    [ObservableProperty]
    private UserProfileDto? _profile;

    public ProfileViewModel(ProfileService profileService, AuthService authService)
    {
        _profileService = profileService;
        _authService = authService;
    }

    [RelayCommand]
    private Task InitializeAsync() => ExecuteSafeAsync(async () =>
    {
        Profile = await _profileService.GetProfileAsync();
    });

    [RelayCommand]
    private void Logout() => _authService.Logout();
}
