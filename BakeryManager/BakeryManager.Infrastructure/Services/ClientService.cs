using BakeryManager.Core.Domain;
using BakeryManager.Core.Repositories;
using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.DTO;
using BakeryManager.Infrastructure.Services.Interfaces;

namespace BakeryManager.Infrastructure.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientsRepository;
    
    public ClientService(IClientRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }
    
    public async Task<int> UpdateClient(int id, CreateClient clientBody)
    {
        if (!IsBodyValid(clientBody))
        {
            return await Task.FromResult(-1);
        }
        
        var client = ParseCreateClientIntoClient(clientBody);
        var result = await _clientsRepository.UpdateAsync(id, client);
        
        return result;
    }

    public async Task<int> DeleteClient(int id)
    {
        var result = await _clientsRepository.DelAsync(id);
        
        return result;
    }

    public async Task<int> AddClient(CreateClient clientBody)
    {
        if (!IsBodyValid(clientBody))
        {
            return -1;
        }
        
        var client = ParseCreateClientIntoClient(clientBody);
        var result = await _clientsRepository.AddAsync(client);

        return result;
    }

    public async Task<ClientDTO?> GetClient(int id)
    {
        var client = await _clientsRepository.GetAsync(id);

        if (client == null)
        {
            return null;
        }
        else
        {
            return ParseClientIntoClientDTO(client);
        }
    }

    public async Task<IEnumerable<ClientDTO>> BrowseAll()
    {
        var clients = await _clientsRepository.BrowseAllAsync();

        var clientsDTOs = clients.Select(client => ParseClientIntoClientDTO(client));

        return clientsDTOs;
    }

    private static bool IsBodyValid(CreateClient body) => body.Name != null && body.Surname != null;
    
    private ClientDTO ParseClientIntoClientDTO(Client client)
    {
        return new ClientDTO()
        {
            Id = client.Id,
            Name = client.Name,
            Surname = client.Surname,
        };
    }

    private Client ParseCreateClientIntoClient(CreateClient clientBody)
    {
        return new Client()
        {
            Name = clientBody.Name!,
            Surname = clientBody.Surname!,
        };
    }
}