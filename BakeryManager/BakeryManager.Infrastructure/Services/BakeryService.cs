using BakeryManager.Core.Repositories;
using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.DTO;
using BakeryManager.Infrastructure.Services.Interfaces;

namespace BakeryManager.Infrastructure.Services;

public class BakeryService : IBakeryService
{
    private readonly IBakeryRepository _bakeryRepository;
    
    public BakeryService(IBakeryRepository bakeryRepository)
    {
        _bakeryRepository = bakeryRepository;
    }


    public async Task<int> UpdateBakery(string bakeryCode, CreateBakery bakeryBody)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteBakery(string bakeryCode)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddBakery(CreateBakery bakeryBody)
    {
        throw new NotImplementedException();
    }

    public async Task<BakeryDTO> GetBakery(string bakeryCode)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BakeryDTO>> BrowseAll()
    {
        throw new NotImplementedException();
    }
}