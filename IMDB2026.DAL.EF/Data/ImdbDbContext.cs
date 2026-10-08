using IMDB2026.DAL.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMDB2026.DAL.EF.Data;

public class ImdbDbContext : DbContext
{
    public ImdbDbContext(DbContextOptions<ImdbDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductHistory> ProductHistories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

       
        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .HasMaxLength(100);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(p => p.Name).HasMaxLength(150);
            entity.Property(p => p.PurchasePrice).HasColumnType("decimal(18, 2)");
            entity.Property(p => p.SellingPrice).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ProductHistory>()
            .Property(ph => ph.Comment)
            .HasMaxLength(255);

        
        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }
}