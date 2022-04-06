using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.Infrastructure.DTO;

public class ReceiptDTO
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