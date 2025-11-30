using BobaTeaApp.Api.Data;
using BobaTeaApp.Api.Entities;
using BobaTeaApp.Shared.Configurations;
using BobaTeaApp.Shared.DTOs;
using BobaTeaApp.Shared.Requests;
using BobaTeaApp.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;

namespace BobaTeaApp.Api.Services;

public sealed class PaymentService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly PaymentIntentService _paymentIntentService;
    private readonly StripeOptions _stripeOptions;

    public PaymentService(ApplicationDbContext dbContext, IOptions<StripeOptions> stripeOptions)
    {
        _dbContext = dbContext;
        _stripeOptions = stripeOptions.Value;
        _paymentIntentService = new PaymentIntentService();
    }

    public async Task<ApiResponse<string>> CreatePaymentIntentAsync(Guid userId, StripePaymentIntentRequest request, CancellationToken cancellationToken = default)
    {
        var paymentIntent = await _paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
        {
            Amount = (long)(request.Amount * 100),
            Currency = request.Currency ?? _stripeOptions.Currency,
            PaymentMethod = request.PaymentMethodToken,
            ConfirmationMethod = "automatic",
            Confirm = true,
            ReceiptEmail = request.ReceiptEmail
        }, cancellationToken: cancellationToken);

        return ApiResponse<string>.Ok(paymentIntent.ClientSecret ?? string.Empty, "Payment intent created");
    }

    public async Task<IReadOnlyList<PaymentMethodDto>> GetPaymentMethodsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var methods = await _dbContext.PaymentMethods
            .Where(m => m.UserId == userId)
            .ToListAsync(cancellationToken);

        return methods.Select(m => new PaymentMethodDto(m.Id, m.Provider, m.MaskedCardNumber, m.ExpMonth, m.ExpYear, m.IsDefault)).ToList();
    }

    public async Task<ApiResponse<PaymentMethodDto>> SavePaymentMethodAsync(Guid userId, SavePaymentMethodRequest request, CancellationToken cancellationToken = default)
    {
        var method = new PaymentMethodEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Provider = "Stripe",
            MaskedCardNumber = "**** **** **** 4242",
            Token = request.PaymentMethodToken,
            ExpMonth = 12,
            ExpYear = DateTime.UtcNow.Year + 4,
            IsDefault = request.SetAsDefault
        };

        if (request.SetAsDefault)
        {
            var existing = await _dbContext.PaymentMethods.Where(m => m.UserId == userId).ToListAsync(cancellationToken);
            foreach (var payment in existing)
            {
                payment.IsDefault = false;
            }
        }

        _dbContext.PaymentMethods.Add(method);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var dto = new PaymentMethodDto(method.Id, method.Provider, method.MaskedCardNumber, method.ExpMonth, method.ExpYear, method.IsDefault);
        return ApiResponse<PaymentMethodDto>.Ok(dto, "Payment method saved");
    }
}
