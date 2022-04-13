using BakeryManager.Infrastructure.ViewsModels;

namespace BakeryManager.Infrastructure.ViewProviders.Interfaces;

public interface IPremiumClientsView
{
    Task<IEnumerable<PremiumClient>> BrowseAllAsync();
}