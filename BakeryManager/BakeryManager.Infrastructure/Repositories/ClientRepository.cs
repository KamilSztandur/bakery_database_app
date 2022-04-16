using BakeryManager.Core.Domain;
using BakeryManager.Core.Repositories;

namespace BakeryManager.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext? _appDbContext;

    public ClientRepository(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }

    public async Task<int> UpdateAsync(int id, Client newClient)
    {
        try
        {
            var editedClient = _appDbContext!.Clients.FirstOrDefault(client => client.Id == id);
            if (editedClient == null)
            {
                return 404;
            }
            else
            {
                editedClient.Name = newClient.Name;
                editedClient.Surname = newClient.Surname;

                var result = await _appDbContext.SaveChangesAsync();
                await Task.CompletedTask;

                return result;   
            }
        }
        catch (Exception e)
        {
            return -1;
        }
    }

    public async Task<int> DelAsync(int id)
    {
        try
        {
            var clientToRemove = _appDbContext!.Clients.FirstOrDefault(client => client.Id == id);
            if (clientToRemove == null)
            {
                return 200;
            }
            else
            {
                _appDbContext!.Remove(clientToRemove);
                var result = await _appDbContext.SaveChangesAsync();

                await Task.CompletedTask;
                return result;
            }
        }
        catch (Exception e)
        {
            return -1;
        }
    }

    public async Task<int> AddAsync(Client client)
    {
        try
        {
            _appDbContext!.Clients.Add(client);
            var result = _appDbContext.SaveChanges();

            await Task.CompletedTask;
            return result;
        }
        catch (Exception e)
        {
            return -1;
        }
    }

    public async Task<Client?> GetAsync(int id)
    {
        try
        {
            var client = await Task.FromResult(
                _appDbContext!.Clients.FirstOrDefault(client => client.Id == id)
            );

            return await Task.FromResult(client);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<IEnumerable<Client>> BrowseAllAsync()
    {
        try
        {
            IEnumerable<Client> clients = await Task.FromResult(_appDbContext!.Clients);

            return clients;
        }
        catch (Exception e)
        {
            return new List<Client>();
        }
    }
}