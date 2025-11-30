using BobaTeaApp.Shared.Configurations;
using BobaTeaApp.Shared.DTOs;
using BobaTeaApp.Shared.Requests;

namespace BobaTeaApp.Mobile.Services;

public sealed class FavoritesService
{
    private readonly IApiClient _apiClient;

    public FavoritesService(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public Task<Guid[]?> GetFavoritesAsync(CancellationToken cancellationToken = default) =>
        _apiClient.GetAsync<Guid[]>(ApiRoutes.Favorites.All, cancellationToken);

    public Task<Guid[]?> AddFavoriteAsync(Guid productId, CancellationToken cancellationToken = default) =>
        _apiClient.PostAsync<CreateFavoriteRequest, Guid[]>(ApiRoutes.Favorites.All, new CreateFavoriteRequest(productId), cancellationToken);

    public Task RemoveFavoriteAsync(Guid productId, CancellationToken cancellationToken = default) =>
        _apiClient.DeleteAsync($"{ApiRoutes.Favorites.All}/{productId}", cancellationToken);
}
