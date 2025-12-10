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
    }
}
