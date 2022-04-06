namespace BakeryManager.Infrastructure.Commands;

public class CreateReceipt
{
    public int ClientId { get; set; }
    
    public int ProductId { get; set; }
    
    public string? BakeryCode { get; set; }
    
    public double TotalPrice { get; set; }
    
    public string? Date { get; set; }
}