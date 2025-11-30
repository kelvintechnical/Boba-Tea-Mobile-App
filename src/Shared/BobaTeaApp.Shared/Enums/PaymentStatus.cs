namespace BobaTeaApp.Shared.Enums;

public enum PaymentStatus
{
    NotRequired = 0,
    Pending = 1,
    Authorized = 2,
    Captured = 3,
    Failed = 4,
    Refunded = 5
}
