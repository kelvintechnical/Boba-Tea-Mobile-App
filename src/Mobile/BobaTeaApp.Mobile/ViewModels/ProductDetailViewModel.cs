using BobaTeaApp.Mobile.Services;
using BobaTeaApp.Shared.DTOs;
using CommunityToolkit.Mvvm.Input;

namespace BobaTeaApp.Mobile.ViewModels;

[QueryProperty(nameof(Product), nameof(Product))]
public partial class ProductDetailViewModel : BaseViewModel
{
    private readonly CartService _cartService;

    [ObservableProperty]
    private ProductDto? _product;

    [ObservableProperty]
    private int _quantity = 1;

    public ProductDetailViewModel(CartService cartService)
    {
        _cartService = cartService;
    }

    partial void OnQuantityChanging(int value)
    {
        if (value < 1)
        {
            Quantity = 1;
        }
    }

    [RelayCommand]
    private void AddToCart()
    {
        if (Product is null) return;

        var item = new CartItemDto(
            Product.Id,
            Product.Name,
            Product.BasePrice,
            Quantity,
            "Regular",
            "100%",
            "Standard",
            null,
            Product.BasePrice * Quantity,
            null);

        _cartService.AddOrUpdate(item);
    }
}
