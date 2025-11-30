using System.Security.Claims;
using BobaTeaApp.Api.Entities;
using BobaTeaApp.Api.Services;
using BobaTeaApp.Shared.Configurations;
using BobaTeaApp.Shared.DTOs;
using BobaTeaApp.Shared.Requests;
using BobaTeaApp.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BobaTeaApp.Api.Controllers;

[ApiController]
[Route(ApiRoutes.Auth.Profile)]
public sealed class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly TokenService _tokenService;

    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost("/api/auth/register")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest(ApiResponse<string>.Fail(string.Join(";", result.Errors.Select(e => e.Description))));
        }

        var (token, expires) = _tokenService.GenerateAccessToken(user);
        return Ok(new AuthResponse(user.Id, user.Email!, user.FullName ?? string.Empty, token, string.Empty, expires));
    }

    [HttpPost("/api/auth/login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Unauthorized(ApiResponse<string>.Fail("Invalid credentials"));
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            return Unauthorized(ApiResponse<string>.Fail("Invalid credentials"));
        }

        var (token, expires) = _tokenService.GenerateAccessToken(user);
        return Ok(new AuthResponse(user.Id, user.Email!, user.FullName ?? string.Empty, token, string.Empty, expires));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserProfileDto>> Profile([FromServices] UserProfileService profileService)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var profile = await profileService.GetAsync(userId);
        return profile is null ? NotFound() : Ok(profile);
    }
}
