using BakeryManager.Infrastructure.Repositories;
using BakeryManager.Infrastructure.ViewProviders.Interfaces;
using BakeryManager.Infrastructure.ViewsModels;

namespace BakeryManager.Infrastructure.ViewProviders;

public class PremiumClientViewProvider  : IPremiumClientsView
{
    private readonly AppDbContext? _appDbContext;

    public PremiumClientViewProvider(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }

    public async Task<IEnumerable<PremiumClient>> BrowseAllAsync()
    {
        try
        {
            var premiumClients = (await Task.FromResult(_appDbContext!.PremiumClientsView)) as IEnumerable<PremiumClient>;

            return premiumClients;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<PremiumClient>();
        }
    }
}