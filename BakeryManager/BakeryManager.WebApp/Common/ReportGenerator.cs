using System.Text;
using BakeryManager.WebApp.Models.ReportsModels;

namespace BakeryManager.WebApp.Common;

public static class ReportGenerator
{
    public static MemoryStream? GetPremiumClientsAsCsv(List<PremiumClientVM> data)
    {
        var builder = new StringBuilder();
        builder.AppendLine("Client ID,Client,Is Premium");

        foreach (var item in data)
        {
            builder.AppendLine($"\"{item.ClientId}\",\"{item.ClientId}\",\"{item.IsPremium}\"");
        }
        
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        
        writer.Write(builder.ToString());
        writer.Flush();

        memoryStream.Position = 0;

        return memoryStream;
    }
    
    public static MemoryStream? GetSoldProductsAsCsv(List<SoldProductVM> data)
    {
        var builder = new StringBuilder();
        builder.AppendLine("Receipt ID, Bakery Code, Customer, Product Name, Total Price, Date");

        foreach (var item in data)
        {
            builder.AppendLine($"\"{item.ReceiptID}\",\"{item.BakeryCode}\",\"{item.Customer}\",\"{item.Name}\",\"{item.TotalPrice}\",\"{item.Date}\"");
        }
        
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        
        writer.Write(builder.ToString());
        writer.Flush();

        memoryStream.Position = 0;

        return memoryStream;
    }
    
    public static MemoryStream? GetClientsExpensesAsCsv(List<ClientExpensesVM> data)
    {
        var builder = new StringBuilder();
        builder.AppendLine("Client ID,Client,Total Expenses, Total Units Bought");

        foreach (var item in data)
        {
            builder.AppendLine($"\"{item.ClientID}\",\"{item.Client}\",\"{item.TotalExpenses}\",\"{item.TotalUnitsBought}\"");
        }
        
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        
        writer.Write(builder.ToString());
        writer.Flush();

        memoryStream.Position = 0;

        return memoryStream;
    }
    
    public static MemoryStream? GetEarningsPerProductAsCsv(List<EarningsPerProductVM> data)
    {
        var builder = new StringBuilder();
        builder.AppendLine("Product ID,Product Name, Price Per Unit, Units Sold, Total Earnings");

        foreach (var item in data)
        {
            builder.AppendLine($"\"{item.Id}\",\"{item.Name}\",\"{item.PricePerUnit}\",\"{item.UnitsSold}\",\"{item.TotalEarnings}\"");
        }
        
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        
        writer.Write(builder.ToString());
        writer.Flush();

        memoryStream.Position = 0;

        return memoryStream;
    }
    
    public static MemoryStream? GetBakeriesEarningsAsCsv(List<BakeryEarningsVM> data)
    {
        var builder = new StringBuilder();
        builder.AppendLine("Bakery Code, Postal Address, Street Address, Units Sold, Total Earnings");

        foreach (var item in data)
        {
            builder.AppendLine($"\"{item.BakeryCode}\",\"{item.PostalAddress}\",\"{item.StreetAddress}\",\"{item.UnitsSold}\",\"{item.TotalEarnings}\"");
        }
        
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        
        writer.Write(builder.ToString());
        writer.Flush();

        memoryStream.Position = 0;

        return memoryStream;
    }
}