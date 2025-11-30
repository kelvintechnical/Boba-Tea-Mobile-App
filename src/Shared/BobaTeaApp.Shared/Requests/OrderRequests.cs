using BobaTeaApp.Shared.DTOs;

namespace BobaTeaApp.Shared.Requests;

public sealed record CreateOrderRequest(
    string PickupName,
    string? Notes,
    IReadOnlyList<CartItemDto> Items,
    string PaymentMethodToken,
    string? CouponCode,
    string Location,
    decimal Subtotal,
    decimal Tax,
    decimal Total);

public sealed record UpdateOrderStatusRequest(
    Guid OrderId,
    string OrderNumber,
    string? EmployeeId,
    string? Notes);
