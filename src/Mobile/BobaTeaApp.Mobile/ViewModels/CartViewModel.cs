using System.Collections.ObjectModel;
using BobaTeaApp.Mobile.Services;
using BobaTeaApp.Shared.DTOs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BobaTeaApp.Mobile.ViewModels;

public partial class CartViewModel : BaseViewModel
{
    private readonly CartService _cartService;

    public ReadOnlyObservableCollection<CartItemDto> Items => _cartService.Items;

    [ObservableProperty]
    private decimal _subtotal;

    [ObservableProperty]
    private bool _isEmpty;

    public CartViewModel(CartService cartService, IToastService toastService) : base(toastService)
    {
        _cartService = cartService;
        _cartService.CartChanged += OnCartChanged;
        UpdateCartState();
    }

    private void OnCartChanged(object? sender, EventArgs e)
    {
        CalculateSubtotal();
        UpdateCartState();
    }

    private void UpdateCartState()
    {
        IsEmpty = !_cartService.Items.Any();
    }

    private void CalculateSubtotal()
    {
        Subtotal = _cartService.Subtotal;
    }

    [RelayCommand]
    private async Task IncrementAsync(CartItemDto item)
    {
        _cartService.UpdateQuantity(item.ProductId, item.Quantity + 1);
        if (ToastService != null)
            await ToastService.ShowInfoAsync("Item quantity updated");
    }

    [RelayCommand]
    private async Task DecrementAsync(CartItemDto item)
    {
        _cartService.UpdateQuantity(item.ProductId, item.Quantity - 1);
        if (ToastService != null)
            await ToastService.ShowInfoAsync("Item quantity updated");
    }

    [RelayCommand]
    private async Task RemoveAsync(CartItemDto item)
    {
        _cartService.UpdateQuantity(item.ProductId, 0);
        if (ToastService != null)
            await ToastService.ShowSuccessAsync("Item removed from cart");
    }

    [RelayCommand]
    private async Task NavigateCheckoutAsync()
    {
        if (IsEmpty)
        {
            if (ToastService != null)
                await ToastService.ShowErrorAsync("Your cart is empty");
            return;
        }
        await Shell.Current.GoToAsync(nameof(Views.CheckoutPage));
    }

    [RelayCommand]
    private async Task NavigateToMenuAsync()
    {
        await Shell.Current.GoToAsync("//MenuPage");
    }
}
