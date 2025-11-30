using BobaTeaApp.Shared.Configurations;
using BobaTeaApp.Shared.Requests;
using BobaTeaApp.Shared.Responses;

namespace BobaTeaApp.Mobile.Services;

public sealed class AuthService
{
    private readonly IApiClient _apiClient;
    private readonly SecureStorageService _secureStorage;

    public AuthService(IApiClient apiClient, SecureStorageService secureStorage)
    {
        _apiClient = apiClient;
        _secureStorage = secureStorage;
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _apiClient.PostAsync<LoginRequest, AuthResponse>(ApiRoutes.Auth.Login, request, cancellationToken);
        if (response != null)
        {
            await _secureStorage.SetAccessTokenAsync(response.AccessToken);
            await _secureStorage.SetRefreshTokenAsync(response.RefreshToken);
        }

        return response;
    }

    public async Task<AuthResponse?> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default) =>
        await _apiClient.PostAsync<RegisterRequest, AuthResponse>(ApiRoutes.Auth.Register, request, cancellationToken);

    public void Logout() => _secureStorage.Clear();
}
