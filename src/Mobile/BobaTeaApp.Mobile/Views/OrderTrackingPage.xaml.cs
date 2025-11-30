using BobaTeaApp.Mobile.ViewModels;

namespace BobaTeaApp.Mobile.Views;

public partial class OrderTrackingPage : ContentPage
{
    public OrderTrackingPage(OrderTrackingViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
