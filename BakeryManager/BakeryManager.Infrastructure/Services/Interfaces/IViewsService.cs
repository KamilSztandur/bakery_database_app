using BakeryManager.Infrastructure.ViewsModels;

namespace BakeryManager.Infrastructure.Services.Interfaces;

public interface IViewsService
{
    Task<IEnumerable<BakeryEarnings>> GetBakeryEarnings();
    
    Task<IEnumerable<ClientExpenses>> GetClientsExpenses();
    
    Task<IEnumerable<EarningsPerProduct>> GetEarningsPerProduct();
    
    Task<IEnumerable<PremiumClient>> GetPremiumClients();
    
    Task<IEnumerable<SoldProduct>> GetSoldProducts();
}