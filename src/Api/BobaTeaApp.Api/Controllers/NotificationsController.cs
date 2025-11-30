using BobaTeaApp.Api.Services;
using BobaTeaApp.Shared.Configurations;
using BobaTeaApp.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BobaTeaApp.Api.Controllers;

[ApiController]
[Authorize]
[Route(ApiRoutes.Notifications.Subscribe)]
public sealed class NotificationsController : ControllerBase
{
    private readonly NotificationService _notificationService;

    public NotificationsController(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost]
    public ActionResult<ApiResponse<string>> Subscribe([FromBody] string deviceToken)
    {
        // Store device token in persistent storage if needed.
        return Ok(ApiResponse<string>.Ok(deviceToken, "Subscribed for push notifications"));
    }
}
