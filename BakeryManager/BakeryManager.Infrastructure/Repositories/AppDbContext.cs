using BakeryManager.Core.Domain;
using BakeryManager.Infrastructure.ViewsModels;
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
    
    public virtual DbSet<BakeryEarnings> BakeryEarningsView { get; set; }
    public virtual DbSet<ClientExpenses> ClientsExpensesView { get; set; }
    public virtual DbSet<EarningsPerProduct> EarningsPerProductView { get; set; }
    public virtual DbSet<PremiumClient> PremiumClientsView { get; set; }
    public virtual DbSet<SoldProduct> SoldProductsView { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("main");
        
        MapTables(modelBuilder);
        MapViews(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void MapTables(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bakery>().HasKey(bakery => bakery.BakeryCode);
        modelBuilder.Entity<Client>().HasKey(client => client.Id);
        modelBuilder.Entity<Discount>().HasKey(discount => discount.Id);
        modelBuilder.Entity<Product>().HasKey(product => product.Id);
        modelBuilder.Entity<Receipt>().HasKey(receipt => receipt.Id);
    }

    private static void MapViews(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BakeryEarnings>(be =>
        {
            be.HasKey(earnings => earnings.BakeryCode);
            be.ToView("BakeriesEarnings");
        });
        
        modelBuilder.Entity<ClientExpenses>(be =>
        {
            be.HasKey(expenses => expenses.ClientID);
            be.ToView("ClientsExpenses");
        });
        
        modelBuilder.Entity<EarningsPerProduct>(be =>
        {
            be.HasKey(earnings => earnings.Id);
            be.ToView("EarningsPerProduct");
        });
        
        modelBuilder.Entity<PremiumClient>(be =>
        {
            be.HasKey(client => client.ClientId);
            be.ToView("PremiumClients");
        });
        
        modelBuilder.Entity<SoldProduct>(be =>
        {
            be.HasKey(soldProduct => soldProduct.ReceiptID);
            be.ToView("SoldProducts");
        });
    }
}