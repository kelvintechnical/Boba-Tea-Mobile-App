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
[Route(ApiRoutes.Payments.Methods)]
public sealed class PaymentsController : ControllerBase
{
    private readonly PaymentService _paymentService;

    public PaymentsController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PaymentMethodDto>>> GetMethods(CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var methods = await _paymentService.GetPaymentMethodsAsync(userId, cancellationToken);
        return Ok(methods);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<PaymentMethodDto>>> SaveMethod([FromBody] SavePaymentMethodRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var response = await _paymentService.SavePaymentMethodAsync(userId, request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("/api/payments/intent")]
    public async Task<ActionResult<ApiResponse<string>>> CreateIntent([FromBody] StripePaymentIntentRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var response = await _paymentService.CreatePaymentIntentAsync(userId, request, cancellationToken);
        return Ok(response);
    }

    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
