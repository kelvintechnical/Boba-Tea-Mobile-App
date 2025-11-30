using BobaTeaApp.Api.Data;
using BobaTeaApp.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BobaTeaApp.Api.Services;

public sealed class MenuService
{
    private readonly ApplicationDbContext _dbContext;

    public MenuService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<MenuCategoryDto>> GetMenuAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _dbContext.Categories
            .Include(c => c.Products)
            .ThenInclude(p => p.Options)
            .OrderBy(c => c.DisplayOrder)
            .ToListAsync(cancellationToken);

        return categories.Select(category => new MenuCategoryDto(
            category.Id,
            category.Name,
            category.Description,
            category.HeroImageUrl,
            category.DisplayOrder,
            category.Products.Select(product => new ProductDto(
                product.Id,
                product.CategoryId,
                product.Name,
                product.Description,
                product.BasePrice,
                product.ImageUrl,
                product.IsAvailable,
                product.IsFeatured,
                product.Calories,
                product.Options.Select(option => new ProductOptionDto(
                    option.Id,
                    option.GroupName,
                    option.Label,
                    option.AdditionalPrice,
                    option.IsDefault)).ToList())).ToList())).ToList();
    }
}
