namespace BobaTeaApp.Shared.Configurations;

public sealed class StripeOptions
{
    public const string SectionName = "Stripe";

    public required string PublishableKey { get; set; }
    public required string SecretKey { get; set; }
    public required string WebhookSecret { get; set; }
    public string Currency { get; set; } = "usd";
}
