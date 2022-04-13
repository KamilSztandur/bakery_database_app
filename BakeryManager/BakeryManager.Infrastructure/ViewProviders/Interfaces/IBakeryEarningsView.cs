using BakeryManager.Infrastructure.ViewsModels;

namespace BakeryManager.Infrastructure.ViewProviders.Interfaces;

public interface IBakeryEarningsView
{
    Task<IEnumerable<BakeryEarnings>> BrowseAllAsync();
}