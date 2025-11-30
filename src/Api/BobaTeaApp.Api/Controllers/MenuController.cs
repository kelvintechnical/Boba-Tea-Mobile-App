using BobaTeaApp.Api.Services;
using BobaTeaApp.Shared.Configurations;
using BobaTeaApp.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BobaTeaApp.Api.Controllers;

[ApiController]
[Route(ApiRoutes.Menu.Categories)]
public sealed class MenuController : ControllerBase
{
    private readonly MenuService _menuService;

    public MenuController(MenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyList<MenuCategoryDto>>> GetMenu(CancellationToken cancellationToken) =>
        Ok(await _menuService.GetMenuAsync(cancellationToken));
}
