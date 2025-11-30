using BobaTeaApp.Shared.Configurations;
using BobaTeaApp.Shared.DTOs;

namespace BobaTeaApp.Mobile.Services;

public sealed class ProfileService
{
    private readonly IApiClient _apiClient;

    public ProfileService(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public Task<UserProfileDto?> GetProfileAsync(CancellationToken cancellationToken = default) =>
        _apiClient.GetAsync<UserProfileDto>(ApiRoutes.Auth.Profile, cancellationToken);
}
