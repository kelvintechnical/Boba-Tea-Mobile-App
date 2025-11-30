using System.Collections.ObjectModel;
using BobaTeaApp.Mobile.Services;
using BobaTeaApp.Shared.DTOs;
using CommunityToolkit.Mvvm.Input;

namespace BobaTeaApp.Mobile.ViewModels;

public partial class CartViewModel : BaseViewModel
{
    private readonly CartService _cartService;

    public ReadOnlyObservableCollection<CartItemDto> Items => _cartService.Items;

    [ObservableProperty]
    private decimal _subtotal;

    public CartViewModel(CartService cartService)
    {
        _cartService = cartService;
        _cartService.CartChanged += (_, _) => CalculateSubtotal();
        CalculateSubtotal();
    }

    private void CalculateSubtotal()
    {
        Subtotal = _cartService.Subtotal;
    }

    [RelayCommand]
    private void Increment(CartItemDto item) =>
        _cartService.UpdateQuantity(item.ProductId, item.Quantity + 1);

    [RelayCommand]
    private void Decrement(CartItemDto item) =>
        _cartService.UpdateQuantity(item.ProductId, item.Quantity - 1);

    [RelayCommand]
    private void Remove(CartItemDto item) =>
        _cartService.UpdateQuantity(item.ProductId, 0);

    [RelayCommand]
    private Task NavigateCheckoutAsync() =>
        Shell.Current.GoToAsync(nameof(Views.CheckoutPage));
}
