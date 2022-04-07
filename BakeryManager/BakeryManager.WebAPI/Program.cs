using BakeryManager.Core.Repositories;
using BakeryManager.Infrastructure.Repositories;
using BakeryManager.Infrastructure.Services;
using BakeryManager.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<IDiscountService, DiscountService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IBakeryRepository, BakeryRepository>();
builder.Services.AddScoped<IBakeryService, BakeryService>();

builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();
builder.Services.AddScoped<IReceiptService, ReceiptService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(
        "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=bakeryapp;Integrated Security=True;TrustServerCertificate=True",
        b => b.MigrationsAssembly("BakeryManager.Infrastructure")
        )
);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.MapControllers();

app.Run();