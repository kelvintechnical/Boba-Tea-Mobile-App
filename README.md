# Boba Tea App

A full-stack boba tea ordering application built with .NET MAUI (mobile) and ASP.NET Core 8 (backend API).

## Project Overview

This is a complete mobile ordering system for a boba tea shop, featuring a cross-platform mobile app for customers and a backend API for order management, payments, and authentication.

### Key Features

- **User Authentication**: JWT-based authentication with ASP.NET Core Identity
- **Menu Browsing**: Browse drinks by category with rich product details
- **Shopping Cart**: Add/remove items, adjust quantities, and manage cart
- **Order Management**: Place orders, track status, view order history
- **Payment Processing**: Stripe integration for secure payments
- **Favorites**: Save favorite drinks for quick reordering
- **User Profiles**: Manage account information and preferences
- **Push Notifications**: Order status updates (stubbed for future implementation)
- **Tax Calculation**: Dynamic tax rates by region

---

## Project Structure

```
BobaTeaApp/
├── src/
│   ├── Api/
│   │   └── BobaTeaApp.Api/              # ASP.NET Core 8 Web API
│   │       ├── Controllers/             # API endpoints
│   │       ├── Services/                # Business logic layer
│   │       ├── Middleware/              # Exception handling, logging
│   │       ├── Data/                    # EF Core DbContext
│   │       └── appsettings.json         # Configuration
│   │
│   ├── Mobile/
│   │   └── BobaTeaApp.Mobile/           # .NET MAUI App
│   │       ├── Views/                   # XAML UI pages
│   │       ├── ViewModels/              # MVVM ViewModels
│   │       ├── Services/                # API clients, auth, cart
│   │       ├── Controls/                # Reusable UI components
│   │       ├── Config/                  # Environment settings
│   │       └── MauiProgram.cs           # DI container setup
│   │
│   └── Shared/
│       └── BobaTeaApp.Shared/           # Shared contracts
│           ├── DTOs/                    # Data Transfer Objects
│           ├── Enums/                   # OrderStatus, PaymentStatus
│           └── Configurations/          # API routes, config models
│
├── sql/
│   └── schema.sql                       # Database schema definition
│
├── Directory.Build.props                # Solution-wide MSBuild properties
├── global.json                          # .NET SDK version
└── BobaTeaApp.sln                       # Solution file
```

---

## Architecture

### Backend API (ASP.NET Core 8)

**Technology Stack:**
- ASP.NET Core 8.0 Web API
- Entity Framework Core 8 (SQL Server)
- ASP.NET Core Identity (Authentication)
- JWT Bearer Tokens (Authorization)
- Stripe SDK (Payments)
- Serilog (Logging)
- Swagger/OpenAPI (Documentation)

**Key Components:**

1. **Controllers** (`src/Api/BobaTeaApp.Api/Controllers/`)
   - `AuthController` - Registration, login, token generation
   - `MenuController` - Product catalog retrieval
   - `OrdersController` - Order placement, tracking, history
   - `CartController` - Tax estimation
   - `PaymentsController` - Stripe payment intent creation
   - `FavoritesController` - Favorite products management
   - `NotificationsController` - Push notification subscriptions
   - `ProfileController` - User profile management

2. **Services** (`src/Api/BobaTeaApp.Api/Services/`)
   - `TokenService` - JWT token generation and validation
   - `MenuService` - Menu data retrieval
   - `OrderWorkflowService` - Order creation, status updates
   - `PaymentService` - Stripe integration, payment method storage
   - `FavoritesService` - Favorites CRUD operations
   - `TaxService` - Tax calculation by region
   - `UserProfileService` - Profile management
   - `NotificationService` - Email/push notifications (stubbed)

3. **Data Layer** (`src/Api/BobaTeaApp.Api/Data/`)
   - `ApplicationDbContext` - EF Core DbContext
   - Entity models: Product, Order, PaymentMethod, UserFavorite, etc.

4. **Middleware**
   - `ExceptionHandlingMiddleware` - Global exception handling

### Mobile App (.NET MAUI)

**Technology Stack:**
- .NET MAUI (Cross-platform: Android, iOS, macOS)
- MVVM Architecture (CommunityToolkit.Mvvm)
- CommunityToolkit.Maui (Toast notifications, UI helpers)
- Dependency Injection (Microsoft.Extensions.DependencyInjection)

**Key Components:**

1. **Views** (`src/Mobile/BobaTeaApp.Mobile/Views/`)
   - `MenuPage` - Browse products by category
   - `CartPage` - Shopping cart with quantity controls
   - `CheckoutPage` - Order review and payment
   - `OrderHistoryPage` - Past orders
   - `OrderTrackingPage` - Real-time order status
   - `ProductDetailPage` - Product customization
   - `FavoritesPage` - Saved favorite drinks
   - `ProfilePage` - User account management
   - `LoginPage` / `RegisterPage` - Authentication

2. **ViewModels** (`src/Mobile/BobaTeaApp.Mobile/ViewModels/`)
   - `BaseViewModel` - Shared loading states, error handling, toast notifications
   - One ViewModel per page (MenuViewModel, CartViewModel, etc.)
   - All use `[RelayCommand]` and `[ObservableProperty]` source generators

3. **Services** (`src/Mobile/BobaTeaApp.Mobile/Services/`)
   - `ApiClient` - HTTP client with token injection
   - `AuthService` - Login, registration, token management
   - `MenuService` - Fetch menu from API
   - `CartService` - In-memory cart management with events
   - `OrderService` - Order placement and tracking
   - `PaymentService` - Stripe payment integration
   - `FavoritesService` - Favorites API wrapper
   - `ProfileService` - User profile API wrapper
   - `SecureStorageService` - Token storage (MAUI SecureStorage)
   - `ToastService` - User feedback via toast notifications
   - `ConnectivityService` - Network status checking

4. **Reusable Controls** (`src/Mobile/BobaTeaApp.Mobile/Controls/`)
   - `LoadingOverlay` - Full-screen loading indicator with message
   - `EmptyStateView` - Empty state UI with icon, title, message, and action button

5. **Configuration**
   - `EnvironmentSettings` - API base URL, mock data toggle
   - Mock data mode for testing without backend

### Shared Library

**Purpose:** Share DTOs, enums, and API routes between Mobile and API projects

**Contents:**
- `DTOs/` - MenuCategoryDto, ProductDto, OrderDto, CartItemDto, etc.
- `Enums/` - OrderStatus, PaymentStatus, NotificationChannel
- `Configurations/` - ApiRoutes (centralized route constants), JwtOptions, StripeOptions

---

## User Experience Enhancements (Latest Updates)

### What Was Added

1. **Toast Notification Service**
   - Location: `src/Mobile/BobaTeaApp.Mobile/Services/ToastService.cs`
   - Provides user feedback for success, error, and info messages
   - Integrated into BaseViewModel for automatic error notifications
   - Uses CommunityToolkit.Maui toast implementation

2. **Enhanced BaseViewModel**
   - Location: `src/Mobile/BobaTeaApp.Mobile/ViewModels/BaseViewModel.cs`
   - Added `ToastService` integration for all ViewModels
   - Enhanced `ExecuteSafeAsync` with:
     - Custom loading messages
     - Success messages
     - Automatic error handling with friendly messages
     - Network error detection (401, 403, 404, 500)
     - Timeout handling
   - Added `LoadingText` property for dynamic loading messages

3. **Loading Overlay Component**
   - Location: `src/Mobile/BobaTeaApp.Mobile/Controls/LoadingOverlay.xaml`
   - Full-screen loading indicator with custom message
   - Semi-transparent background to block interaction
   - Centered spinner with text

4. **Empty State Component**
   - Location: `src/Mobile/BobaTeaApp.Mobile/Controls/EmptyStateView.xaml`
   - Configurable icon, title, message
   - Optional action button with command binding
   - Used for empty cart, no favorites, etc.

5. **Enhanced CartPage**
   - Location: `src/Mobile/BobaTeaApp.Mobile/Views/CartPage.xaml`
   - Empty state when cart is empty ("Your cart is empty" with "Browse Menu" button)
   - Loading overlay during operations
   - Enhanced cart item cards with:
     - Product name and customizations (size, sweetness, ice level)
     - Quantity controls (-, +, Remove buttons)
     - Better visual hierarchy and spacing
   - Toast notifications for cart actions

6. **Enhanced CartViewModel**
   - Location: `src/Mobile/BobaTeaApp.Mobile/ViewModels/CartViewModel.cs`
   - Added `IsEmpty` property to track cart state
   - Toast notifications for increment, decrement, remove actions
   - Navigation guard to prevent checkout with empty cart
   - `NavigateToMenuCommand` for empty state action

7. **Enhanced MenuViewModel**
   - Location: `src/Mobile/BobaTeaApp.Mobile/ViewModels/MenuViewModel.cs`
   - Pull-to-refresh support (`IsRefreshing`, `RefreshCommand`)
   - Loading messages ("Loading menu...")
   - Toast notification when item added to cart
   - Better error handling with user-friendly messages

8. **Dependency Injection Updates**
   - Location: `src/Mobile/BobaTeaApp.Mobile/MauiProgram.cs`
   - Registered `IToastService` as singleton
   - Added `UseMauiCommunityToolkit()` for toast support
   - Updated all ViewModels to inject `IToastService`

---

## Current Status

### ✅ Completed Features

- [x] Complete backend API with all major endpoints
- [x] JWT authentication and authorization
- [x] Full MVVM mobile app architecture
- [x] Shopping cart management
- [x] Order placement workflow
- [x] Stripe payment integration (structure)
- [x] Database schema designed
- [x] Toast notification system
- [x] Loading states and error handling
- [x] Empty state UI components
- [x] Reusable UI controls (LoadingOverlay, EmptyStateView)
- [x] Pull-to-refresh support (partially implemented)
- [x] User-friendly error messages

### 🚧 In Progress / Partially Complete

- [ ] Menu page visual enhancements (started, not fully implemented)
- [ ] Pull-to-refresh on all list pages
- [ ] Image placeholders and loading skeletons
- [ ] Input validation with real-time feedback
- [ ] Success animations
- [ ] Navigation visual feedback improvements

### ❌ Not Yet Implemented

**Critical (Blocks Running):**
- [ ] SDK version mismatch (global.json requires .NET 8, but only 9/10 installed)
- [ ] No EF Core migrations (database can't be created)
- [ ] No seed data (menu will be empty)
- [ ] Missing appsettings.Development.json (placeholders need real values)
- [ ] Missing mobile resources (fonts, images folders referenced but empty)
- [ ] Mobile API base URL points to non-existent production URL

**Stub Implementations:**
- [ ] NotificationService (email/push not wired)
- [ ] PushNotificationService (platform-specific code missing)
- [ ] Payment method persistence (hardcoded card data)
- [ ] Tax service integration in mobile checkout

**Production Readiness:**
- [ ] Refresh token endpoint
- [ ] Order status update endpoint
- [ ] Stripe webhook handler
- [ ] Input validation (DataAnnotations)
- [ ] Pagination (menu, orders, favorites)
- [ ] CORS configuration
- [ ] Rate limiting
- [ ] Unit/integration tests
- [ ] CI/CD workflows
- [ ] Cart persistence (SQLite)
- [ ] Offline support
- [ ] Image caching

---

## UI/UX Appearance

### Design System

**Colors** (defined in `src/Mobile/BobaTeaApp.Mobile/Resources/Styles/Colors.xaml`):
- Primary: Boba tea brand color
- Gray scale: Gray100 - Gray900
- Semantic colors: Danger (for errors/warnings)

**Typography:**
- Bold headings for section titles
- Regular body text for descriptions
- Monospace for order numbers/codes

**Components:**
- Rounded corners (8-16px) for modern look
- Subtle shadows for depth
- Card-based layouts for content grouping
- Consistent spacing (8, 12, 16, 24px increments)

### Current Page Appearance

**CartPage:**
- Empty state with friendly icon and message
- Product cards with name, customizations, price
- Circular quantity buttons (-, +)
- Remove button in subtle red
- Sticky subtotal and checkout button at bottom
- Loading overlay blocks interaction during operations

**MenuPage (Existing):**
- Category-based grouping
- Product cards with image, name, description, price
- "Add to Cart" button per product
- Basic layout (ready for visual enhancements)

**Other Pages:**
- Basic functional layouts
- Need visual polish and empty states
- Loading states not fully wired

---

## Next Steps (Priority Order)

### Phase 1: Get It Running (CRITICAL)

1. **Fix SDK Version Mismatch**
   - Update `global.json` to version 9.0.100 or install .NET 8 SDK
   - Current blocker for building the solution

2. **Create Database Migrations**
   ```bash
   dotnet ef migrations add InitialCreate -p src/Api/BobaTeaApp.Api
   dotnet ef database update -p src/Api/BobaTeaApp.Api
   ```

3. **Create Development Configuration**
   - Copy `appsettings.json` to `appsettings.Development.json`
   - Generate secure JWT signing key (32+ characters)
   - Add Stripe test keys from dashboard.stripe.com
   - Set SQL Server connection string

4. **Seed Sample Data**
   - Add DbContext seeding for categories, products, options
   - At minimum: 2-3 categories, 5-10 products, tax rates

5. **Add Missing Mobile Resources**
   - Create `Resources/Fonts/` and `Resources/Images/` folders
   - Add placeholder images or remove from .csproj

6. **Update Mobile Environment Settings**
   - Change `ApiBaseUrl` to localhost (Android: `http://10.0.2.2:49751`)
   - Enable `UseMockData = true` for testing without API

### Phase 2: Complete UX Enhancements

7. **Finish MenuPage Visual Updates**
   - Apply enhanced styling (started but interrupted)
   - Add pull-to-refresh
   - Add loading overlay

8. **Add Empty States to Remaining Pages**
   - OrderHistoryPage: "No orders yet"
   - FavoritesPage: "No favorites saved"
   - OrderTrackingPage: "Order not found"

9. **Implement Cart Persistence**
   - Add SQLite support
   - Persist cart on app close
   - Restore cart on app launch

10. **Add Input Validation**
    - Real-time validation on login/register forms
    - Visual feedback for invalid inputs
    - DataAnnotations on request models

### Phase 3: Production Readiness

11. **Complete Notification System**
    - Wire up SendGrid for email receipts
    - Implement Firebase Cloud Messaging
    - Store device tokens in database

12. **Add Missing API Endpoints**
    - Refresh token endpoint
    - Order status update (admin)
    - Stripe webhook handler

13. **Security Hardening**
    - Validate JWT signing key is secure
    - Add CORS policy
    - Add rate limiting
    - Enforce HTTPS

14. **Testing**
    - Add xUnit/NUnit test projects
    - Unit tests for services
    - Integration tests for API

15. **CI/CD**
    - GitHub Actions for builds
    - Automated testing
    - Deployment to Azure/AWS

---

## Getting Started

### Prerequisites

- .NET SDK 8.0 or 9.0
- SQL Server (LocalDB, Express, or full)
- Visual Studio 2022 or VS Code
- For mobile development:
  - MAUI workloads: `dotnet workload install maui-android maui-ios`
  - Android SDK (for Android)
  - Xcode (for iOS, macOS only)

### Backend Setup

1. **Update Configuration**
   ```bash
   cd src/Api/BobaTeaApp.Api
   cp appsettings.json appsettings.Development.json
   # Edit appsettings.Development.json with real values
   ```

2. **Create Database**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

3. **Run API**
   ```bash
   dotnet run
   ```
   - API: https://localhost:49750
   - Swagger: https://localhost:49750/swagger

### Mobile Setup

1. **Update Environment Settings**
   - Edit `src/Mobile/BobaTeaApp.Mobile/Config/EnvironmentSettings.cs`
   - Set `ApiBaseUrl` to localhost
   - Set `UseMockData = true` (until API is running)

2. **Add Resources**
   - Create `Resources/Fonts` and `Resources/Images` folders
   - Or remove references from `.csproj`

3. **Run App**
   ```bash
   cd src/Mobile/BobaTeaApp.Mobile
   dotnet build
   dotnet run -f net8.0-android  # For Android
   dotnet run -f net8.0-ios      # For iOS (Mac only)
   ```

---

## API Endpoints

### Authentication
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login (returns JWT)

### Menu
- `GET /api/menu` - Get all categories and products

### Cart
- `POST /api/cart/estimate` - Get tax estimate

### Orders
- `POST /api/orders` - Place new order
- `GET /api/orders` - Get user's order history
- `GET /api/orders/{orderNumber}/track` - Track order by number

### Payments
- `POST /api/payments/create-intent` - Create Stripe payment intent
- `GET /api/payments/methods` - Get saved payment methods
- `POST /api/payments/methods` - Save payment method
- `PUT /api/payments/methods/{id}/default` - Set default payment

### Favorites
- `GET /api/favorites` - Get user's favorites
- `POST /api/favorites` - Add favorite
- `DELETE /api/favorites/{productId}` - Remove favorite

### Notifications
- `POST /api/notifications/subscribe` - Subscribe to push notifications

### Profile
- `GET /api/profile` - Get user profile
- `PUT /api/profile` - Update profile

---

## Database Schema

See `sql/schema.sql` for complete schema definition.

**Key Tables:**
- `AspNetUsers` - User accounts (Identity)
- `ProductCategories` - Menu categories
- `Products` - Boba tea products
- `ProductOptions` - Size, ice level, sweetness options
- `Orders` - Customer orders
- `OrderItems` - Line items per order
- `PaymentMethods` - Saved Stripe payment methods
- `UserFavorites` - User's favorite products
- `TaxRates` - Tax rates by region

---

## Configuration

### Backend (appsettings.json)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BobaTeaApp;..."
  },
  "JwtOptions": {
    "Issuer": "BobaTeaAppApi",
    "Audience": "BobaTeaAppMobile",
    "SigningKey": "CHANGE_ME_TO_SECURE_KEY",
    "ExpiryMinutes": 60
  },
  "StripeOptions": {
    "PublishableKey": "pk_test_...",
    "SecretKey": "sk_test_..."
  }
}
```

### Mobile (EnvironmentSettings.cs)

```csharp
ApiBaseUrl = "https://api.bloom-boba.com"  // Change to localhost for dev
UseMockData = false                        // Set true to test without API
```

---

## Technology Highlights

**Why .NET MAUI?**
- Single codebase for Android, iOS, macOS, Windows
- Native performance and UI
- Full C# and XAML support
- Shared business logic with backend

**Why ASP.NET Core?**
- High performance
- Built-in dependency injection
- Entity Framework Core for database
- Excellent authentication/authorization support

**Why MVVM?**
- Clean separation of concerns
- Testable business logic
- Two-way data binding
- Community toolkit source generators reduce boilerplate

---

## Contributing

This is a learning project. Key areas for improvement:

1. Complete the stub implementations (notifications, payments)
2. Add comprehensive testing
3. Implement offline support
4. Add more UX polish (animations, transitions)
5. Improve error handling and logging
6. Add analytics and monitoring
7. Implement admin dashboard

---

## License

This is a learning/demonstration project. Use as you wish.

---

## Contact

For questions or feedback, please open an issue in the GitHub repository.

---

**Last Updated:** 2025-11-30
**Status:** In Development - UX Enhancements Phase
