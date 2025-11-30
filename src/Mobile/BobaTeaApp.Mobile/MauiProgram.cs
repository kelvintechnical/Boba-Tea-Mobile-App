using BobaTeaApp.Mobile.Config;
using BobaTeaApp.Mobile.Services;
using BobaTeaApp.Mobile.ViewModels;
using BobaTeaApp.Mobile.Views;
using BobaTeaApp.Shared.Configurations;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace BobaTeaApp.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit();

        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<EnvironmentSettings>();
        builder.Services.AddOptions<EnvironmentSettings>()
            .Configure((settings, configuration) => { });

        builder.Services.AddHttpClient<IApiClient, ApiClient>((sp, client) =>
        {
            var env = sp.GetRequiredService<EnvironmentSettings>();
            client.BaseAddress = new Uri(env.ApiBaseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        builder.Services.AddSingleton<IToastService, ToastService>();
        builder.Services.AddSingleton<SecureStorageService>();
        builder.Services.AddSingleton<AuthService>();
        builder.Services.AddSingleton<MenuService>();
        builder.Services.AddSingleton<CartService>();
        builder.Services.AddSingleton<OrderService>();
        builder.Services.AddSingleton<PaymentService>();
        builder.Services.AddSingleton<PushNotificationService>();
        builder.Services.AddSingleton<ConnectivityService>();
        builder.Services.AddSingleton<FavoritesService>();
        builder.Services.AddSingleton<ProfileService>();

        builder.Services.AddTransient<MenuViewModel>();
        builder.Services.AddTransient<CartViewModel>();
        builder.Services.AddTransient<OrderHistoryViewModel>();
        builder.Services.AddTransient<FavoritesViewModel>();
        builder.Services.AddTransient<ProfileViewModel>();
        builder.Services.AddTransient<ProductDetailViewModel>();
        builder.Services.AddTransient<OrderTrackingViewModel>();
        builder.Services.AddTransient<CheckoutViewModel>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();

        builder.Services.AddTransient<MenuPage>();
        builder.Services.AddTransient<CartPage>();
        builder.Services.AddTransient<OrderHistoryPage>();
        builder.Services.AddTransient<FavoritesPage>();
        builder.Services.AddTransient<ProfilePage>();
        builder.Services.AddTransient<ProductDetailPage>();
        builder.Services.AddTransient<OrderTrackingPage>();
        builder.Services.AddTransient<CheckoutPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<RegisterPage>();

        return builder.Build();
    }
}
