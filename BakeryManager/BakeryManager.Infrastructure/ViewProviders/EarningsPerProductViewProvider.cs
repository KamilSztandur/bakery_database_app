using BakeryManager.Core.Domain;
using BakeryManager.Infrastructure.Repositories;
using BakeryManager.Infrastructure.ViewProviders.Interfaces;
using BakeryManager.Infrastructure.ViewsModels;

namespace BakeryManager.Infrastructure.ViewProviders;

public class EarningsPerProductViewProvider  : IEarningsPerProductView
{
    private readonly AppDbContext? _appDbContext;

    public EarningsPerProductViewProvider(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }

    public async Task<IEnumerable<EarningsPerProduct>> BrowseAllAsync()
    {
        try
        {
            var earningsPerProduct = (await Task.FromResult(_appDbContext!.EarningsPerProductView)) as IEnumerable<EarningsPerProduct>;

            return earningsPerProduct;
        }
        catch (Exception e)
        {
            return new List<EarningsPerProduct>();
        }
    }
}