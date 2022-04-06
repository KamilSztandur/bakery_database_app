using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.Core.Domain;

public class Receipt
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