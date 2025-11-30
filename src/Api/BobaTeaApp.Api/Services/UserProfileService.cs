using BobaTeaApp.Api.Data;
using BobaTeaApp.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BobaTeaApp.Api.Services;

public sealed class UserProfileService
{
    private readonly ApplicationDbContext _dbContext;

    public UserProfileService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserProfileDto?> GetAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user == null) return null;

        var paymentMethods = await _dbContext.PaymentMethods
            .Where(m => m.UserId == userId)
            .Select(m => new PaymentMethodDto(m.Id, m.Provider, m.MaskedCardNumber, m.ExpMonth, m.ExpYear, m.IsDefault))
            .ToListAsync(cancellationToken);

        var favorites = await _dbContext.Favorites
            .Where(f => f.UserId == userId)
            .Select(f => f.ProductId)
            .ToListAsync(cancellationToken);

        return new UserProfileDto(user.Id, user.Email ?? string.Empty, user.FullName ?? string.Empty, user.AvatarUrl, user.PhoneNumber, paymentMethods, favorites);
    }
}
