using System.Security.Claims;
using BobaTeaApp.Api.Services;
using BobaTeaApp.Shared.Configurations;
using BobaTeaApp.Shared.DTOs;
using BobaTeaApp.Shared.Requests;
using BobaTeaApp.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BobaTeaApp.Api.Controllers;

[ApiController]
[Authorize]
[Route(ApiRoutes.Orders.Create)]
public sealed class OrdersController : ControllerBase
{
    private readonly OrderWorkflowService _orderWorkflowService;

    public OrdersController(OrderWorkflowService orderWorkflowService)
    {
        _orderWorkflowService = orderWorkflowService;
    }

    [HttpPost]
    public async Task<ActionResult<OrderDetailDto>> Create([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var order = await _orderWorkflowService.CreateOrderAsync(userId, request, cancellationToken);
        return CreatedAtAction(nameof(Track), new { orderNumber = order.OrderNumber }, order);
    }

    [HttpGet("history")]
    public async Task<ActionResult<IReadOnlyList<OrderSummaryDto>>> History(CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var history = await _orderWorkflowService.GetHistoryAsync(userId, cancellationToken);
        return Ok(history);
    }

    [HttpGet("{orderNumber}")]
    public async Task<ActionResult<OrderStatusResponse>> Track(string orderNumber, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var status = await _orderWorkflowService.TrackAsync(userId, orderNumber, cancellationToken);
        return status is null ? NotFound() : Ok(status);
    }

    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
