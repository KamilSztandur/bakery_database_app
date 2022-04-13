using BakeryManager.Infrastructure.Services.Interfaces;
using BakeryManager.Infrastructure.ViewProviders.Interfaces;
using BakeryManager.Infrastructure.ViewsModels;

namespace BakeryManager.Infrastructure.Services;

public class ViewsService : IViewsService
{
    private readonly IBakeryEarningsView _bakeryEarningsView;
    private readonly IClientsExpensesView _clientsExpensesView;
    private readonly IEarningsPerProductView _earningsPerProductView;
    private readonly IPremiumClientsView _premiumClientsView;
    private readonly ISoldProductsView _soldProductsView;
    
    public ViewsService(
        IBakeryEarningsView bakeryEarningsView,
        IClientsExpensesView clientsExpensesView,
        IEarningsPerProductView earningsPerProductView,
        IPremiumClientsView premiumClientsView,
        ISoldProductsView soldProductsView
    )
    {
        _bakeryEarningsView = bakeryEarningsView;
        _clientsExpensesView = clientsExpensesView;
        _earningsPerProductView = earningsPerProductView;
        _premiumClientsView = premiumClientsView;
        _soldProductsView = soldProductsView;
    }
    
    public async Task<IEnumerable<BakeryEarnings>> GetBakeryEarnings()
    {
        var bakeryEarnings = await _bakeryEarningsView.BrowseAllAsync();
        
        return bakeryEarnings;
    }

    public async Task<IEnumerable<ClientExpenses>> GetClientsExpenses()
    {
        var clientsExpenses = await _clientsExpensesView.BrowseAllAsync();
        
        return clientsExpenses;
    }

    public async Task<IEnumerable<EarningsPerProduct>> GetEarningsPerProduct()
    {
        var earningsPerProduct = await _earningsPerProductView.BrowseAllAsync();
        
        return earningsPerProduct;
    }

    public async Task<IEnumerable<PremiumClient>> GetPremiumClients()
    {
        var premiumClients = await _premiumClientsView.BrowseAllAsync();
        
        return premiumClients;
    }

    public async Task<IEnumerable<SoldProduct>> GetSoldProducts()
    {
        var soldProducts = await _soldProductsView.BrowseAllAsync();
        
        return soldProducts;
    }
}