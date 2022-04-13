using BakeryManager.Infrastructure.ViewsModels;

namespace BakeryManager.Infrastructure.ViewProviders.Interfaces;

public interface IEarningsPerProductView
{
    Task<IEnumerable<EarningsPerProduct>> BrowseAllAsync();
}