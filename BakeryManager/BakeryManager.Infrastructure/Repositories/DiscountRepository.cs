using BakeryManager.Core.Domain;
using BakeryManager.Core.Repositories;

namespace BakeryManager.Infrastructure.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly AppDbContext? _appDbContext;

    public DiscountRepository(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }
    
    public async Task<int> UpdateAsync(int id, Discount discount)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DelAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddAsync(Discount discount)
    {
        throw new NotImplementedException();
    }

    public async Task<Discount> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Discount>> BrowseAllAsync()
    {
        throw new NotImplementedException();
    }
}