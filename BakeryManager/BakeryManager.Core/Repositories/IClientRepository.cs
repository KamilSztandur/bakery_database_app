using BakeryManager.Core.Domain;

namespace BakeryManager.Core.Repositories;

public interface IClientRepository
{
    Task<int> UpdateAsync(int id, Client client);

    Task<int> DelAsync(int id);

    Task<int> AddAsync(Client client);

    Task<Client?> GetAsync(int id);

    Task<IEnumerable<Client>> BrowseAllAsync();
}