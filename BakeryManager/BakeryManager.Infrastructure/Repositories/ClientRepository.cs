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
                return await Task.FromResult(404);
            }
            else
            {
                editedClient.Name = newClient.Name;
                editedClient.Surname = newClient.Surname;

                var result = _appDbContext.SaveChanges();
                await Task.CompletedTask;

                return await Task.FromResult(result);   
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return await Task.FromResult(-1);
        }
    }

    public async Task<int> DelAsync(int id)
    {
        try
        {
            var clientToRemove = _appDbContext!.Clients.FirstOrDefault(client => client.Id == id);
            if (clientToRemove == null)
            {
                return await Task.FromResult(200);
            }
            else
            {
                _appDbContext!.Remove(clientToRemove);
                var result = _appDbContext.SaveChanges();

                await Task.CompletedTask;
                return await Task.FromResult(result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return await Task.FromResult(-1);
        }
    }

    public async Task<int> AddAsync(Client client)
    {
        try
        {
            _appDbContext!.Clients.Add(client);
            var result = _appDbContext.SaveChanges();

            await Task.CompletedTask;
            return await Task.FromResult(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return await Task.FromResult(-1);
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
            Console.WriteLine(e.Message);
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
            Console.WriteLine(e.Message);
            return new List<Client>();
        }
    }
}