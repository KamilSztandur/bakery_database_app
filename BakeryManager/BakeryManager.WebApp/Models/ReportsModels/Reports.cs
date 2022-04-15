namespace BakeryManager.WebApp.Models.ReportsModels;

public class Reports
{
    public List<BakeryEarningsVM> BakeriesEarnings { get; set; }
    
    public List<ClientExpensesVM> ClientsExpenses { get; set; }
    
    public List<EarningsPerProductVM> EarningsPerProduct { get; set; }
    
    public List<PremiumClientVM> PremiumClients  { get; set; }
    
    public List<SoldProductVM> SoldProducts  { get; set; }
}