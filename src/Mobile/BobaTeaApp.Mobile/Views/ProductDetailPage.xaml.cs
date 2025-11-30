using BobaTeaApp.Mobile.ViewModels;

namespace BobaTeaApp.Mobile.Views;

public partial class ProductDetailPage : ContentPage
{
    public ProductDetailPage(ProductDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
