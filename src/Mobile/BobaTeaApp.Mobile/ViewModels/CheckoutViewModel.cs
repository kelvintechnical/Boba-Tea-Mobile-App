using BobaTeaApp.Mobile.Services;
using BobaTeaApp.Shared.DTOs;
using BobaTeaApp.Shared.Requests;
using CommunityToolkit.Mvvm.Input;

namespace BobaTeaApp.Mobile.ViewModels;

public partial class CheckoutViewModel : BaseViewModel
{
    private readonly CartService _cartService;
    private readonly OrderService _orderService;
    private readonly PaymentService _paymentService;

    [ObservableProperty]
    private string _pickupName = string.Empty;

    [ObservableProperty]
    private string _notes = string.Empty;

    [ObservableProperty]
    private decimal _tax;

    [ObservableProperty]
    private decimal _total;

    public ReadOnlyObservableCollection<CartItemDto> Items => _cartService.Items;

    public CheckoutViewModel(CartService cartService, OrderService orderService, PaymentService paymentService)
    {
        _cartService = cartService;
        _orderService = orderService;
        _paymentService = paymentService;
        CalculateTotals();
        _cartService.CartChanged += (_, _) => CalculateTotals();
    }

    private void CalculateTotals()
    {
        var subtotal = _cartService.Subtotal;
        Tax = Math.Round(subtotal * 0.0825m, 2);
        Total = subtotal + Tax;
    }

    [RelayCommand]
    private Task SubmitAsync() => ExecuteSafeAsync(async () =>
    {
        if (!Items.Any())
        {
            throw new InvalidOperationException("Your cart is empty.");
        }

        var request = new CreateOrderRequest(
            PickupName,
            string.IsNullOrWhiteSpace(Notes) ? null : Notes,
            Items.ToList(),
            "default",
            null,
            "default",
            _cartService.Subtotal,
            Tax,
            Total);

        await _orderService.CreateOrderAsync(request);
        _cartService.Clear();
    });
}
