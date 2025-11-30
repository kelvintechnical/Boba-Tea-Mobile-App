using System.Security.Claims;
using BobaTeaApp.Api.Services;
using BobaTeaApp.Shared.Configurations;
using BobaTeaApp.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BobaTeaApp.Api.Controllers;

[ApiController]
[Authorize]
[Route(ApiRoutes.Favorites.All)]
public sealed class FavoritesController : ControllerBase
{
    private readonly FavoritesService _favoritesService;

    public FavoritesController(FavoritesService favoritesService)
    {
        _favoritesService = favoritesService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Guid>>> Get(CancellationToken cancellationToken)
    {
        var favorites = await _favoritesService.GetAsync(GetUserId(), cancellationToken);
        return Ok(favorites);
    }

    [HttpPost]
    public async Task<ActionResult<IReadOnlyList<Guid>>> Add([FromBody] CreateFavoriteRequest request, CancellationToken cancellationToken)
    {
        var favorites = await _favoritesService.AddAsync(GetUserId(), request.ProductId, cancellationToken);
        return Ok(favorites);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> Remove(Guid productId, CancellationToken cancellationToken)
    {
        await _favoritesService.RemoveAsync(GetUserId(), productId, cancellationToken);
        return NoContent();
    }

    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
