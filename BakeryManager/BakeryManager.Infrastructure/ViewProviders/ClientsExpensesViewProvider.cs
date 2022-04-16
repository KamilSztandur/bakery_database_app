using BakeryManager.Infrastructure.Repositories;
using BakeryManager.Infrastructure.ViewProviders.Interfaces;
using BakeryManager.Infrastructure.ViewsModels;

namespace BakeryManager.Infrastructure.ViewProviders;

public class ClientExpensesViewProvider  : IClientsExpensesView
{
    private readonly AppDbContext? _appDbContext;

    public ClientExpensesViewProvider(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }

    public async Task<IEnumerable<ClientExpenses>> BrowseAllAsync()
    {
        try
        {
            var clientsExpenses = (await Task.FromResult(_appDbContext!.ClientsExpensesView)) as IEnumerable<ClientExpenses>;

            return clientsExpenses;
        }
        catch (Exception e)
        {
            return new List<ClientExpenses>();
        }
    }
}