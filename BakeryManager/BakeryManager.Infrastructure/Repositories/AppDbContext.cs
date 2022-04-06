using BakeryManager.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace BakeryManager.Infrastructure.Repositories;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    
    public DbSet<Bakery> Bakeries { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Product>  Products { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("bakery");
        modelBuilder.Entity<Bakery>().ToTable("BAKERIES");
        modelBuilder.Entity<Client>().ToTable("CLIENTS");
        modelBuilder.Entity<Discount>().ToTable("DISCOUNTS");
        modelBuilder.Entity<Product>().ToTable("PRODUCTS");
        modelBuilder.Entity<Receipt>().ToTable("RECEIPTS");
        
        base.OnModelCreating(modelBuilder);
    }
}