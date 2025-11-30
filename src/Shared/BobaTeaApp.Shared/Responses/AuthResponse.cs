namespace BobaTeaApp.Shared.Responses;

public sealed record AuthResponse(
    Guid UserId,
    string Email,
    string FullName,
    string AccessToken,
    string RefreshToken,
    DateTimeOffset ExpiresAt);
