namespace BakeryManager.Infrastructure.ViewsModels;

public class ClientExpenses
{
    public int ClientID { get; set; }
    
    public string Client { get; set; }
    
    public double TotalExpenses { get; set; }
    
    public int TotalUnitsBought { get; set; }
}