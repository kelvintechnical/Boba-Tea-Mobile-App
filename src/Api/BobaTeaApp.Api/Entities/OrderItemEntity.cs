namespace BobaTeaApp.Api.Entities;

public sealed class OrderItemEntity
{
    public Guid Id { get; set; }
    public required Guid OrderId { get; set; }
    public required Guid ProductId { get; set; }
    public required string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string? SelectedOptions { get; set; }
    public decimal LineTotal { get; set; }

    public OrderEntity? Order { get; set; }
}
