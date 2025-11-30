using BobaTeaApp.Mobile.ViewModels;

namespace BobaTeaApp.Mobile.Views;

public partial class CartPage : ContentPage
{
    public CartPage(CartViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
