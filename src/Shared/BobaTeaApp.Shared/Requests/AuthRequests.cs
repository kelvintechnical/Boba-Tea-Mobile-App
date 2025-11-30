namespace BobaTeaApp.Shared.Requests;

public sealed record LoginRequest(string Email, string Password);

public sealed record RegisterRequest(
    string Email,
    string Password,
    string FullName,
    string? PhoneNumber);
