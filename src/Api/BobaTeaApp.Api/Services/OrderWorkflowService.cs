using BobaTeaApp.Api.Data;
using BobaTeaApp.Api.Entities;
using BobaTeaApp.Shared.DTOs;
using BobaTeaApp.Shared.Enums;
using BobaTeaApp.Shared.Requests;
using BobaTeaApp.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace BobaTeaApp.Api.Services;

public sealed class OrderWorkflowService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly NotificationService _notificationService;

    public OrderWorkflowService(ApplicationDbContext dbContext, NotificationService notificationService)
    {
        _dbContext = dbContext;
        _notificationService = notificationService;
    }

    public async Task<OrderDetailDto> CreateOrderAsync(Guid userId, CreateOrderRequest request, CancellationToken cancellationToken = default)
    {
        var orderNumber = $"BB-{DateTimeOffset.UtcNow:yyyyMMddHHmmss}";
        var order = new OrderEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            OrderNumber = orderNumber,
            Status = OrderStatus.Pending,
            PaymentStatus = PaymentStatus.Pending,
            PickupName = request.PickupName,
            Notes = request.Notes,
            Subtotal = request.Subtotal,
            Tax = request.Tax,
            Total = request.Total,
            EstimatedReadyTime = DateTimeOffset.UtcNow.AddMinutes(12)
        };

        var orderItems = request.Items.Select(item => new OrderItemEntity
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            ProductId = item.ProductId,
            ProductName = item.Name,
            UnitPrice = item.UnitPrice,
            Quantity = item.Quantity,
            SelectedOptions = item.SelectedOptions,
            LineTotal = item.LineTotal
        }).ToList();

        order.Items = orderItems;

        await _dbContext.Orders.AddAsync(order, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var dto = MapOrder(order);
        await _notificationService.SendOrderPlacedAsync(order.UserId, dto);
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        await _notificationService.SendReceiptEmailAsync(user?.Email ?? string.Empty, dto);
        return dto;
    }

    public async Task<IReadOnlyList<OrderSummaryDto>> GetHistoryAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var orders = await _dbContext.Orders
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(cancellationToken);

        return orders.Select(o => new OrderSummaryDto(
            o.Id,
            o.OrderNumber,
            o.Status,
            o.Total,
            o.CreatedAt,
            o.EstimatedReadyTime)).ToList();
    }

    public async Task<OrderStatusResponse?> TrackAsync(Guid userId, string orderNumber, CancellationToken cancellationToken = default)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber && o.UserId == userId, cancellationToken);
        return order == null
            ? null
            : new OrderStatusResponse(order.Id, order.OrderNumber, order.Status, order.PaymentStatus, order.EstimatedReadyTime, order.Notes);
    }

    private static OrderDetailDto MapOrder(OrderEntity order) => new(
        order.Id,
        order.OrderNumber,
        order.Status,
        order.PaymentStatus,
        order.Subtotal,
        order.Tax,
        order.Total,
        order.Notes,
        order.CreatedAt,
        order.EstimatedReadyTime,
        order.Items.Select(item => new OrderItemDto(
            item.ProductId,
            item.ProductName,
            item.Quantity,
            item.UnitPrice,
            item.SelectedOptions,
            item.LineTotal)).ToList());
}
