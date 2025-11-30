using BobaTeaApp.Shared.Enums;

namespace BobaTeaApp.Shared.DTOs;

public sealed record OrderSummaryDto(
    Guid Id,
    string OrderNumber,
    OrderStatus Status,
    decimal Total,
    DateTimeOffset CreatedAt,
    DateTimeOffset? EstimatedReadyTime);

public sealed record OrderDetailDto(
    Guid Id,
    string OrderNumber,
    OrderStatus Status,
    PaymentStatus PaymentStatus,
    decimal Subtotal,
    decimal Tax,
    decimal Total,
    string? Notes,
    DateTimeOffset CreatedAt,
    DateTimeOffset? EstimatedReadyTime,
    IReadOnlyList<OrderItemDto> Items);

public sealed record OrderItemDto(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    string? SelectedOptions,
    decimal LineTotal);
