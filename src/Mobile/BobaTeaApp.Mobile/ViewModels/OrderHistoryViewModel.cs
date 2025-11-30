using System.Collections.ObjectModel;
using BobaTeaApp.Mobile.Services;
using BobaTeaApp.Shared.DTOs;
using CommunityToolkit.Mvvm.Input;

namespace BobaTeaApp.Mobile.ViewModels;

public partial class OrderHistoryViewModel : BaseViewModel
{
    private readonly OrderService _orderService;

    public ObservableCollection<OrderSummaryDto> Orders { get; } = new();

    public OrderHistoryViewModel(OrderService orderService)
    {
        _orderService = orderService;
    }

    [RelayCommand]
    private Task InitializeAsync() => ExecuteSafeAsync(async () =>
    {
        var history = await _orderService.GetHistoryAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Orders.Clear();
            foreach (var order in history.OrderByDescending(o => o.CreatedAt))
            {
                Orders.Add(order);
            }
        });
    });

    [RelayCommand]
    private Task TrackAsync(string orderNumber) =>
        Shell.Current.GoToAsync($"{nameof(Views.OrderTrackingPage)}?orderNumber={orderNumber}");
}
