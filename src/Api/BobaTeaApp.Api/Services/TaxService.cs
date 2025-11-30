using BobaTeaApp.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace BobaTeaApp.Api.Services;

public sealed class TaxService
{
    private readonly ApplicationDbContext _dbContext;

    public TaxService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<decimal> CalculateTaxAsync(string region, decimal amount, CancellationToken cancellationToken = default)
    {
        var rate = await _dbContext.TaxRates.FirstOrDefaultAsync(r => r.Region == region, cancellationToken);
        return rate == null ? 0 : Math.Round(amount * rate.Rate, 2);
    }
}
