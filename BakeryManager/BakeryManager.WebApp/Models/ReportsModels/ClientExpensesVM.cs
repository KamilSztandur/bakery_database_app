namespace BakeryManager.WebApp.Models.ReportsModels;

public class ClientExpensesVM
{
    public int ClientID { get; set; }
    
    public string Client { get; set; }
    
    public double TotalExpenses { get; set; }
    
    public int TotalUnitsBought { get; set; }
}