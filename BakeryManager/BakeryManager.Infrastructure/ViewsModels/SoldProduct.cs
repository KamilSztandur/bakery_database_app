namespace BakeryManager.Infrastructure.ViewsModels;

public class SoldProduct
{
    public int ReceiptID { get; set; }
    
    public string BakeryCode { get; set; }
    
    public string Customer { get; set; }
    
    public string Name { get; set; }
    
    public double TotalPrice { get; set; }
    
    public DateTime Date { get; set; }
}