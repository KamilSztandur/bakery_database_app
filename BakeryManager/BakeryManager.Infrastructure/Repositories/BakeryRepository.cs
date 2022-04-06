using BakeryManager.Core.Domain;
using BakeryManager.Core.Repositories;

namespace BakeryManager.Infrastructure.Repositories;

public class BakeryRepository : IBakeryRepository
{
    private readonly AppDbContext? _appDbContext;

    public BakeryRepository(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }
    
    public async Task<int> UpdateAsync(string bakeryCode, Bakery bakery)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DelAsync(string bakeryCode)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddAsync(Bakery bakery)
    {
        throw new NotImplementedException();
    }

    public async Task<Bakery> GetAsync(string bakeryCode)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Bakery>> BrowseAllAsync()
    {
        throw new NotImplementedException();
    }
}