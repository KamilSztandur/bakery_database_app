namespace BakeryManager.WebApp.Models.ReportsModels;

public class SoldProductVM
{
    public int ReceiptID { get; set; }
    
    public string BakeryCode { get; set; }
    
    public string Customer { get; set; }
    
    public string Name { get; set; }
    
    public double TotalPrice { get; set; }
    
    public DateTime Date { get; set; }
}