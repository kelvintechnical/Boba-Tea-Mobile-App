namespace BobaTeaApp.Mobile.Config;

public sealed class EnvironmentSettings
{
    public string ApiBaseUrl { get; set; } = "https://api.bloom-boba.com";
    public string StripePublishableKey { get; set; } = string.Empty;
    public bool EnableLogging { get; set; } = true;
    public bool UseMockData { get; set; } = true;
}
