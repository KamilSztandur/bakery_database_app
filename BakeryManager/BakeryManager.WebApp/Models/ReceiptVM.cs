using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.WebApp.Models;

public class ReceiptVM
{
    [NotNull]
    public int Id { get; set; }
    
    [NotNull]
    public int ClientId { get; set; }
    
    [NotNull]
    public int ProductId { get; set; }
    
    [NotNull]
    public string BakeryCode { get; set; }
    
    [NotNull]
    public double TotalPrice { get; set; }
    
    [NotNull]
    public DateTime Date { get; set; }
}