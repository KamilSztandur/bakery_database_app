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
        modelBuilder.HasDefaultSchema("main");
        modelBuilder.Entity<Bakery>().HasKey(bakery => bakery.BakeryCode);
        modelBuilder.Entity<Client>().HasKey(client => client.Id);
        modelBuilder.Entity<Discount>().HasKey(discount => discount.Id);
        modelBuilder.Entity<Product>().HasKey(product => product.Id);
        modelBuilder.Entity<Receipt>().HasKey(receipt => receipt.Id);
        /*
        modelBuilder.Entity<Bakery>().ToTable("BAKERIES").HasKey(bakery => bakery.BakeryCode);
        modelBuilder.Entity<Client>().ToTable("CLIENTS").HasKey(client => client.Id);
        modelBuilder.Entity<Discount>().ToTable("DISCOUNTS").HasKey(discount => discount.Id);
        modelBuilder.Entity<Product>().ToTable("PRODUCTS").HasKey(product => product.Id);
        modelBuilder.Entity<Receipt>().ToTable("RECEIPTS").HasKey(receipt => receipt.Id);
        */
        base.OnModelCreating(modelBuilder);
    }
}