using System.Collections.ObjectModel;
using BobaTeaApp.Mobile.Services;
using BobaTeaApp.Shared.DTOs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BobaTeaApp.Mobile.ViewModels;

public partial class MenuViewModel : BaseViewModel
{
    private readonly MenuService _menuService;
    private readonly CartService _cartService;

    public ObservableCollection<MenuCategoryDto> Categories { get; } = new();

    [ObservableProperty]
    private bool _isRefreshing;

    public MenuViewModel(MenuService menuService, CartService cartService, IToastService toastService)
        : base(toastService)
    {
        _menuService = menuService;
        _cartService = cartService;
    }

    [RelayCommand]
    private Task InitializeAsync() => ExecuteSafeAsync(
        async () =>
        {
            if (Categories.Any()) return;
            await LoadMenuAsync();
        },
        loadingMessage: "Loading menu...",
        showErrorToast: true
    );

    [RelayCommand]
    private async Task RefreshAsync()
    {
        IsRefreshing = true;
        try
        {
            await LoadMenuAsync();
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    private async Task LoadMenuAsync()
    {
        var categories = await _menuService.GetMenuAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Categories.Clear();
            foreach (var category in categories.OrderBy(c => c.DisplayOrder))
            {
                Categories.Add(category);
            }
        });
    }

    [RelayCommand]
    private async Task AddToCartAsync(ProductDto product)
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

        if (ToastService != null)
            await ToastService.ShowSuccessAsync($"{product.Name} added to cart");
    }
}
