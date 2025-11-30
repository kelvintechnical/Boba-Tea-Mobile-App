using BobaTeaApp.Shared.Configurations;
using BobaTeaApp.Shared.DTOs;
using BobaTeaApp.Shared.Requests;
using BobaTeaApp.Shared.Responses;

namespace BobaTeaApp.Mobile.Services;

public sealed class PaymentService
{
    private readonly IApiClient _apiClient;

    public PaymentService(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public Task<PaymentMethodDto[]?> GetSavedMethodsAsync(CancellationToken cancellationToken = default) =>
        _apiClient.GetAsync<PaymentMethodDto[]>(ApiRoutes.Payments.Methods, cancellationToken);

    public Task<ApiResponse<string>?> CreatePaymentIntentAsync(StripePaymentIntentRequest request, CancellationToken cancellationToken = default) =>
        _apiClient.PostAsync<StripePaymentIntentRequest, ApiResponse<string>>(ApiRoutes.Payments.PaymentIntent, request, cancellationToken);

    public Task<ApiResponse<PaymentMethodDto>?> SaveMethodAsync(SavePaymentMethodRequest request, CancellationToken cancellationToken = default) =>
        _apiClient.PostAsync<SavePaymentMethodRequest, ApiResponse<PaymentMethodDto>>(ApiRoutes.Payments.Methods, request, cancellationToken);
}
