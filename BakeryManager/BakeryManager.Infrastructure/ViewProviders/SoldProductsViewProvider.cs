using BakeryManager.Infrastructure.Repositories;
using BakeryManager.Infrastructure.ViewProviders.Interfaces;
using BakeryManager.Infrastructure.ViewsModels;

namespace BakeryManager.Infrastructure.ViewProviders;

public class SoldProductViewProvider  : ISoldProductsView
{
    private readonly AppDbContext? _appDbContext;

    public SoldProductViewProvider(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }

    public async Task<IEnumerable<SoldProduct>> BrowseAllAsync()
    {
        try
        {
            var soldProducts = (await Task.FromResult(_appDbContext!.SoldProductsView)) as IEnumerable<SoldProduct>;

            return soldProducts;
        }
        catch (Exception e)
        {
            return new List<SoldProduct>();
        }
    }
}