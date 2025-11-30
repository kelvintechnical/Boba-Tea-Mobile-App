using BobaTeaApp.Api.Services;
using BobaTeaApp.Shared.Configurations;
using BobaTeaApp.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BobaTeaApp.Api.Controllers;

[ApiController]
[Authorize]
[Route(ApiRoutes.Cart.TaxEstimate)]
public sealed class CartController : ControllerBase
{
    private readonly TaxService _taxService;

    public CartController(TaxService taxService)
    {
        _taxService = taxService;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<decimal>>> Estimate([FromQuery] string region, [FromQuery] decimal amount, CancellationToken cancellationToken)
    {
        var tax = await _taxService.CalculateTaxAsync(region, amount, cancellationToken);
        return Ok(ApiResponse<decimal>.Ok(tax));
    }
}
