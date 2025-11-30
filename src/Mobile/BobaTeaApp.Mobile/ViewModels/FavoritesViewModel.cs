using System.Collections.ObjectModel;
using BobaTeaApp.Mobile.Services;
using CommunityToolkit.Mvvm.Input;

namespace BobaTeaApp.Mobile.ViewModels;

public partial class FavoritesViewModel : BaseViewModel
{
    private readonly FavoritesService _favoritesService;

    public ObservableCollection<Guid> FavoriteProductIds { get; } = new();

    public FavoritesViewModel(FavoritesService favoritesService)
    {
        _favoritesService = favoritesService;
    }

    [RelayCommand]
    private Task InitializeAsync() => ExecuteSafeAsync(async () =>
    {
        var favorites = await _favoritesService.GetFavoritesAsync();
        if (favorites == null) return;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            FavoriteProductIds.Clear();
            foreach (var id in favorites)
            {
                FavoriteProductIds.Add(id);
            }
        });
    });
}
