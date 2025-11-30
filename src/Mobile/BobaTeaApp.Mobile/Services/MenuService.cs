using BobaTeaApp.Shared.Configurations;
using BobaTeaApp.Shared.DTOs;

namespace BobaTeaApp.Mobile.Services;

public sealed class MenuService
{
    private readonly IApiClient _apiClient;
    private readonly EnvironmentSettings _environment;

    public MenuService(IApiClient apiClient, EnvironmentSettings environment)
    {
        _apiClient = apiClient;
        _environment = environment;
    }

    public async Task<IReadOnlyList<MenuCategoryDto>> GetMenuAsync(CancellationToken cancellationToken = default)
    {
        if (_environment.UseMockData)
        {
            return MockMenu();
        }

        return await _apiClient.GetAsync<IReadOnlyList<MenuCategoryDto>>(ApiRoutes.Menu.Categories, cancellationToken)
               ?? Array.Empty<MenuCategoryDto>();
    }

    private static IReadOnlyList<MenuCategoryDto> MockMenu()
    {
        var classicCategoryId = Guid.Parse("0b1a6140-846d-4f2f-87ae-17826dfec66a");
        var fruitCategoryId = Guid.Parse("4220e7ec-6c0c-4e58-828f-fea16f940ef9");

        var classics = new MenuCategoryDto(
            classicCategoryId,
            "Classic Milk Tea",
            "Slow-brewed teas with premium milk",
            "https://images.bloom-boba.com/classic-tea.png",
            1,
            new List<ProductDto>
            {
                new(
                    Guid.Parse("8b6bc24d-7488-49f1-8e53-6f3ede556f1c"),
                    classicCategoryId,
                    "Brown Sugar Brûlée",
                    "Caramelized brown sugar syrup, milk foam, boba",
                    6.50m,
                    "https://images.bloom-boba.com/brown-sugar.png",
                    true,
                    true,
                    280,
                    Array.Empty<ProductOptionDto>()),
                new(
                    Guid.Parse("e1a53c9b-05c4-4f87-9db1-ae0c7f8db0b1"),
                    classicCategoryId,
                    "Jasmine Green Milk",
                    "Floral jasmine tea with oat milk option",
                    5.95m,
                    "https://images.bloom-boba.com/jasmine.png",
                    true,
                    false,
                    180,
                    Array.Empty<ProductOptionDto>())
            });

        var fruitTeas = new MenuCategoryDto(
            fruitCategoryId,
            "Fruit Tea",
            "Vibrant fruit infusions with popping pearls",
            "https://images.bloom-boba.com/fruit-tea.png",
            2,
            new List<ProductDto>
            {
                new(
                    Guid.Parse("4fd5da8e-696d-48a2-b9aa-575fe471f741"),
                    fruitCategoryId,
                    "Lychee Breeze",
                    "Lychee puree, jasmine tea, aloe",
                    6.25m,
                    "https://images.bloom-boba.com/lychee.png",
                    true,
                    true,
                    160,
                    Array.Empty<ProductOptionDto>())
            });

        return new[] { classics, fruitTeas };
    }
}
