using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.DTO;

namespace BakeryManager.Infrastructure.Services.Interfaces;

public interface IClientService
{
    Task<int> UpdateClient(int id, CreateClient clientBody);
    Task<int> DeleteClient(int id);
    Task<int> AddClient(CreateClient clientBody);
    Task<ClientDTO?> GetClient(int id);
    Task<IEnumerable<ClientDTO>> BrowseAll();
}