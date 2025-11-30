namespace BobaTeaApp.Shared.Requests;

public sealed record SavePaymentMethodRequest(
    string PaymentMethodToken,
    bool SetAsDefault);

public sealed record StripePaymentIntentRequest(
    decimal Amount,
    string Currency,
    string PaymentMethodToken,
    string ReceiptEmail);
