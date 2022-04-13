using BakeryManager.Core.Repositories;
using BakeryManager.Infrastructure.Repositories;
using BakeryManager.Infrastructure.Services;
using BakeryManager.Infrastructure.Services.Interfaces;
using BakeryManager.Infrastructure.ViewProviders;
using BakeryManager.Infrastructure.ViewProviders.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Tables
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

// Views
builder.Services.AddScoped<IBakeryEarningsView, BakeryEarningsViewProvider>();
builder.Services.AddScoped<IClientsExpensesView, ClientExpensesViewProvider>();
builder.Services.AddScoped<IEarningsPerProductView, EarningsPerProductViewProvider>();
builder.Services.AddScoped<IPremiumClientsView, PremiumClientViewProvider>();
builder.Services.AddScoped<ISoldProductsView, SoldProductViewProvider>();
builder.Services.AddScoped<IViewsService, ViewsService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(
        "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=bakeryapp;Integrated Security=True;TrustServerCertificate=True",
        b => b.MigrationsAssembly("BakeryManager.Infrastructure")
        )
);

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.MapControllers();

app.Run();