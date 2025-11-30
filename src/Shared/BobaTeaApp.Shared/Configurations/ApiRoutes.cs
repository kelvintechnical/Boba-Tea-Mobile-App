namespace BobaTeaApp.Shared.Configurations;

public static class ApiRoutes
{
    public const string Base = "/api";

    public static class Auth
    {
        public const string Login = Base + "/auth/login";
        public const string Register = Base + "/auth/register";
        public const string Refresh = Base + "/auth/refresh";
        public const string Profile = Base + "/auth/profile";
    }

    public static class Menu
    {
        public const string Categories = Base + "/menu";
        public const string Product = Base + "/menu/{id}";
    }

    public static class Orders
    {
        public const string Create = Base + "/orders";
        public const string History = Base + "/orders/history";
        public const string Track = Base + "/orders/{orderNumber}";
        public const string UpdateStatus = Base + "/orders/{orderNumber}/status";
    }

    public static class Cart
    {
        public const string TaxEstimate = Base + "/cart/tax";
    }

    public static class Payments
    {
        public const string PaymentIntent = Base + "/payments/intent";
        public const string Methods = Base + "/payments/methods";
    }

    public static class Favorites
    {
        public const string All = Base + "/favorites";
    }

    public static class Notifications
    {
        public const string Subscribe = Base + "/notifications/subscribe";
    }
}
