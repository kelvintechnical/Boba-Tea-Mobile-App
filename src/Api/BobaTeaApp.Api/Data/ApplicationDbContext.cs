using BobaTeaApp.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BobaTeaApp.Api.Data;

public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ProductCategoryEntity> Categories => Set<ProductCategoryEntity>();
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
    public DbSet<ProductOptionEntity> ProductOptions => Set<ProductOptionEntity>();
    public DbSet<OrderEntity> Orders => Set<OrderEntity>();
    public DbSet<OrderItemEntity> OrderItems => Set<OrderItemEntity>();
    public DbSet<PaymentMethodEntity> PaymentMethods => Set<PaymentMethodEntity>();
    public DbSet<UserFavoriteEntity> Favorites => Set<UserFavoriteEntity>();
    public DbSet<TaxRateEntity> TaxRates => Set<TaxRateEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ProductEntity>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        builder.Entity<ProductOptionEntity>()
            .HasOne(o => o.Product)
            .WithMany(p => p.Options)
            .HasForeignKey(o => o.ProductId);

        builder.Entity<OrderItemEntity>()
            .HasOne(i => i.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(i => i.OrderId);

        builder.Entity<UserFavoriteEntity>()
            .HasIndex(f => new { f.UserId, f.ProductId })
            .IsUnique();

        builder.Entity<OrderEntity>()
            .Property(o => o.Subtotal)
            .HasPrecision(18, 2);

        builder.Entity<OrderEntity>()
            .Property(o => o.Tax)
            .HasPrecision(18, 2);

        builder.Entity<OrderEntity>()
            .Property(o => o.Total)
            .HasPrecision(18, 2);

        builder.Entity<OrderItemEntity>()
            .Property(i => i.UnitPrice)
            .HasPrecision(18, 2);

        builder.Entity<OrderItemEntity>()
            .Property(i => i.LineTotal)
            .HasPrecision(18, 2);

        builder.Entity<ProductEntity>()
            .Property(p => p.BasePrice)
            .HasPrecision(18, 2);

        builder.Entity<ProductEntity>()
            .Property(p => p.Calories)
                .HasPrecision(6, 2);

        builder.Entity<ProductOptionEntity>()
            .Property(o => o.AdditionalPrice)
            .HasPrecision(18, 2);

        builder.Entity<TaxRateEntity>()
            .Property(t => t.Rate)
            .HasPrecision(5, 4);
    }
}
