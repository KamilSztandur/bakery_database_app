using BakeryManager.Infrastructure.Repositories;
using BakeryManager.Infrastructure.ViewProviders.Interfaces;
using BakeryManager.Infrastructure.ViewsModels;

namespace BakeryManager.Infrastructure.ViewProviders;

public class BakeryEarningsViewProvider  : IBakeryEarningsView
{
    private readonly AppDbContext? _appDbContext;

    public BakeryEarningsViewProvider(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }

    public async Task<IEnumerable<BakeryEarnings>> BrowseAllAsync()
    {
        try
        {
            var bakeriesEarnings = (await Task.FromResult(_appDbContext!.BakeryEarningsView)) as IEnumerable<BakeryEarnings>;

            return bakeriesEarnings;
        }
        catch (Exception e)
        {
            return new List<BakeryEarnings>();
        }
    }
}