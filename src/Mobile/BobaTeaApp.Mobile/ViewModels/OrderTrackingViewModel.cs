using BobaTeaApp.Mobile.Services;
using BobaTeaApp.Shared.Responses;
using CommunityToolkit.Mvvm.Input;

namespace BobaTeaApp.Mobile.ViewModels;

[QueryProperty(nameof(OrderNumber), nameof(OrderNumber))]
public partial class OrderTrackingViewModel : BaseViewModel
{
    private readonly OrderService _orderService;

    [ObservableProperty]
    private string _orderNumber = string.Empty;

    [ObservableProperty]
    private OrderStatusResponse? _status;

    public OrderTrackingViewModel(OrderService orderService)
    {
        _orderService = orderService;
    }

    [RelayCommand]
    private Task TrackAsync() => ExecuteSafeAsync(async () =>
    {
        if (string.IsNullOrWhiteSpace(OrderNumber))
        {
            throw new InvalidOperationException("Provide an order number");
        }

        Status = await _orderService.TrackAsync(OrderNumber);
    });
}
