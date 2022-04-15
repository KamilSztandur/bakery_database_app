namespace BakeryManager.WebApp.Models.ReportsModels;

public class EarningsPerProductVM
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public double PricePerUnit { get; set; }
    
    public int UnitsSold { get; set; }
    
    public double TotalEarnings { get; set; }
}