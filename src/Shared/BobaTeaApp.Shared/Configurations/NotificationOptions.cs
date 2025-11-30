namespace BobaTeaApp.Shared.Configurations;

public sealed class NotificationOptions
{
    public const string SectionName = "Notifications";

    public string? FirebaseServerKey { get; set; }
    public string? FirebaseSenderId { get; set; }
    public string? EmailFromAddress { get; set; }
    public string? SendGridApiKey { get; set; }
}
