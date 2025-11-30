using BobaTeaApp.Shared.Enums;

namespace BobaTeaApp.Api.Entities;

public sealed class OrderEntity
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required string OrderNumber { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public string? Notes { get; set; }
    public string? PickupName { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? EstimatedReadyTime { get; set; }

    public ApplicationUser? User { get; set; }
    public ICollection<OrderItemEntity> Items { get; set; } = new List<OrderItemEntity>();
}
