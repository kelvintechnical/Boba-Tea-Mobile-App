using System.Collections.ObjectModel;
using BobaTeaApp.Mobile.Services;
using BobaTeaApp.Shared.DTOs;
using CommunityToolkit.Mvvm.Input;

namespace BobaTeaApp.Mobile.ViewModels;

public partial class MenuViewModel : BaseViewModel
{
    private readonly MenuService _menuService;
    private readonly CartService _cartService;

    public ObservableCollection<MenuCategoryDto> Categories { get; } = new();

    public MenuViewModel(MenuService menuService, CartService cartService)
    {
        _menuService = menuService;
        _cartService = cartService;
    }

    [RelayCommand]
    private Task InitializeAsync() => ExecuteSafeAsync(async () =>
    {
        if (Categories.Any()) return;
        var categories = await _menuService.GetMenuAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Categories.Clear();
            foreach (var category in categories.OrderBy(c => c.DisplayOrder))
            {
                Categories.Add(category);
            }
        });
    });

    [RelayCommand]
    private void AddToCart(ProductDto product)
    {
        var item = new CartItemDto(
            product.Id,
            product.Name,
            product.BasePrice,
            1,
            "Regular",
            "100%",
            "Standard",
            product.BasePrice,
            null);

        _cartService.AddOrUpdate(item);
    }
}
