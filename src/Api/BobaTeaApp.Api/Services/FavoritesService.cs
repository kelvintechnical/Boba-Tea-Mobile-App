using BobaTeaApp.Api.Data;
using BobaTeaApp.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BobaTeaApp.Api.Services;

public sealed class FavoritesService
{
    private readonly ApplicationDbContext _dbContext;

    public FavoritesService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Guid>> GetAsync(Guid userId, CancellationToken cancellationToken = default) =>
        await _dbContext.Favorites.Where(f => f.UserId == userId).Select(f => f.ProductId).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Guid>> AddAsync(Guid userId, Guid productId, CancellationToken cancellationToken = default)
    {
        var exists = await _dbContext.Favorites.AnyAsync(f => f.UserId == userId && f.ProductId == productId, cancellationToken);
        if (!exists)
        {
            _dbContext.Favorites.Add(new UserFavoriteEntity { Id = Guid.NewGuid(), UserId = userId, ProductId = productId });
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return await GetAsync(userId, cancellationToken);
    }

    public async Task RemoveAsync(Guid userId, Guid productId, CancellationToken cancellationToken = default)
    {
        var favorite = await _dbContext.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId, cancellationToken);
        if (favorite == null) return;
        _dbContext.Favorites.Remove(favorite);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
