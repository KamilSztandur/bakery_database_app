namespace BakeryManager.Infrastructure.ViewsModels;

public class BakeryEarnings
{
    public string BakeryCode { get; set; }
    
    public string PostalAddress { get; set; }
    
    public string StreetAddress { get; set; }
    
    public int UnitsSold { get; set; }
    
    public double TotalEarnings { get; set; }
}