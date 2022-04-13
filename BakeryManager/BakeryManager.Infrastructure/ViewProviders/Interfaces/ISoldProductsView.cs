using BakeryManager.Infrastructure.ViewsModels;

namespace BakeryManager.Infrastructure.ViewProviders.Interfaces;

public interface ISoldProductsView
{
    Task<IEnumerable<SoldProduct>> BrowseAllAsync();
}