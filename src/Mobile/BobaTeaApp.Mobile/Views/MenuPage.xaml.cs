using BobaTeaApp.Mobile.ViewModels;

namespace BobaTeaApp.Mobile.Views;

public partial class MenuPage : ContentPage
{
    private readonly MenuViewModel _viewModel;

    public MenuPage(MenuViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _viewModel.InitializeCommand.ExecuteAsync(null);
    }
}
