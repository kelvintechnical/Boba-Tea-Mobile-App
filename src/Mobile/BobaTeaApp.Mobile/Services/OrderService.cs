using BobaTeaApp.Shared.Configurations;
using BobaTeaApp.Shared.DTOs;
using BobaTeaApp.Shared.Requests;
using BobaTeaApp.Shared.Responses;

namespace BobaTeaApp.Mobile.Services;

public sealed class OrderService
{
    private readonly IApiClient _apiClient;

    public OrderService(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public Task<OrderDetailDto?> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default) =>
        _apiClient.PostAsync<CreateOrderRequest, OrderDetailDto>(ApiRoutes.Orders.Create, request, cancellationToken);

    public async Task<IReadOnlyList<OrderSummaryDto>> GetHistoryAsync(CancellationToken cancellationToken = default) =>
        await _apiClient.GetAsync<IReadOnlyList<OrderSummaryDto>>(ApiRoutes.Orders.History, cancellationToken)
        ?? Array.Empty<OrderSummaryDto>();

    public Task<OrderStatusResponse?> TrackAsync(string orderNumber, CancellationToken cancellationToken = default) =>
        _apiClient.GetAsync<OrderStatusResponse>(ApiRoutes.Orders.Track.Replace("{orderNumber}", orderNumber), cancellationToken);
}
