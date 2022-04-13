using BakeryManager.Infrastructure.ViewsModels;

namespace BakeryManager.Infrastructure.ViewProviders.Interfaces;

public interface IClientsExpensesView
{
    Task<IEnumerable<ClientExpenses>> BrowseAllAsync();
}