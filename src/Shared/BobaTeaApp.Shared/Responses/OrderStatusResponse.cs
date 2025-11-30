using BobaTeaApp.Shared.Enums;

namespace BobaTeaApp.Shared.Responses;

public sealed record OrderStatusResponse(
    Guid OrderId,
    string OrderNumber,
    OrderStatus Status,
    PaymentStatus PaymentStatus,
    DateTimeOffset? EstimatedReadyTime,
    string? Message);
