using System.Text;
using BobaTeaApp.Api.Data;
using BobaTeaApp.Api.Entities;
using BobaTeaApp.Api.Services;
using BobaTeaApp.Shared.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BobaTeaApp.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.SignIn.RequireConfirmedEmail = false;
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddSignInManager()
        .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection(JwtOptions.SectionName);
        services.Configure<JwtOptions>(jwtSection);
        var jwtOptions = jwtSection.Get<JwtOptions>() ?? throw new InvalidOperationException("JWT config missing");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
            };
        });

        return services;
    }

    public static IServiceCollection AddStripeServices(this IServiceCollection services, IConfiguration configuration)
    {
        var stripeSection = configuration.GetSection(StripeOptions.SectionName);
        services.Configure<StripeOptions>(stripeSection);
        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<StripeOptions>>().Value;
            Stripe.StripeConfiguration.ApiKey = options.SecretKey;
            return Stripe.StripeConfiguration.ApiKey;
        });

        services.AddScoped<PaymentService>();
        services.AddScoped<MenuService>();
        services.AddScoped<OrderWorkflowService>();
        services.AddScoped<FavoritesService>();
        services.AddScoped<UserProfileService>();
        services.AddScoped<NotificationService>();
        services.AddScoped<TaxService>();

        return services;
    }
}
