# Bloom Boba App – Setup Guide

## Prerequisites
- .NET 8 SDK and workloads for MAUI, Android, iOS
- Android/iOS emulators or devices
- SQL Server (LocalDB ok for dev)
- Stripe account + API keys
- Firebase/FCM credentials for push notifications

## Solution structure
```
BobaTeaApp.sln
├─ src/Shared         // Shared DTOs, configs, contracts
├─ src/Mobile         // .NET MAUI client
└─ src/Api            // ASP.NET Core backend
```

## Backend API
1. Copy `src/Api/BobaTeaApp.Api/appsettings.json` to `appsettings.Development.json` and override secrets (connection strings, JWT key, Stripe keys).
2. Run EF Core migrations (create these with `dotnet ef migrations add InitialCreate -p src/Api/BobaTeaApp.Api -s src/Api/BobaTeaApp.Api`).
3. Apply migrations: `dotnet ef database update -p src/Api/BobaTeaApp.Api -s src/Api/BobaTeaApp.Api`.
4. Seed menu data (use `sql/schema.sql` as reference or create a seeder).
5. Launch API: `dotnet run --project src/Api/BobaTeaApp.Api`
6. Swagger UI available at `/swagger` (dev only).

### Environment
- JWT settings must use long random signing key.
- Stripe keys: set publishable/secret/webhook values.
- Notifications: set Firebase/SendGrid configuration if used.

## Mobile App
1. Configure `src/Mobile/BobaTeaApp.Mobile/Config/EnvironmentSettings.cs` or create `EnvironmentSettings.json` using .NET MAUI configuration providers to point to API base URL and Stripe publishable key.
2. Restore workloads: `dotnet workload install maui-android maui-ios` (if not installed).
3. Build & run for Android: `dotnet build src/Mobile/BobaTeaApp.Mobile/BobaTeaApp.Mobile.csproj -t:Run -f net8.0-android`.
4. For iOS/macOS, build with `-f net8.0-ios` or `net8.0-maccatalyst` respectively (requires macOS).

### Features implemented
- Clean MVVM (CommunityToolkit) with DI for services/viewmodels.
- Menu browsing, cart, checkout, favorites, order history, profile.
- Auth, payments, push notifications (client stubs + backend endpoints).
- Order tracking, status updates, toast-friendly error messaging.
- Configurable environment + secure token storage.

## Deployment tips
- Replace mock menu data on mobile by setting `EnvironmentSettings.UseMockData = false`.
- Use CI/CD (GitHub Actions) to build API and publish to Azure App Service/Container Apps.
- For mobile store builds, configure entitlements, push certs, signing configs.
- Add real push implementation (FCM/APNS) by wiring `PushNotificationService` + backend subscribe endpoint.

## Testing
- Unit/integration tests can be added under `tests/` (not included).
- Use Swagger or REST client to verify API endpoints, ensure Stripe test keys.
- Manual run-through on Android/iOS for UI/UX verification.
