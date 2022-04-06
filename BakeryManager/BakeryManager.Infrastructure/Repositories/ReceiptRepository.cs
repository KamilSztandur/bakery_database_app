using BakeryManager.Core.Domain;
using BakeryManager.Core.Repositories;

namespace BakeryManager.Infrastructure.Repositories;

public class ReceiptRepository : IReceiptRepository
{
    private readonly AppDbContext? _appDbContext;

    public ReceiptRepository(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }
    
    public async Task<int> UpdateAsync(int id, Receipt receipt)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DelAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddAsync(Receipt receipt)
    {
        throw new NotImplementedException();
    }

    public async Task<Receipt> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Receipt>> BrowseAllAsync()
    {
        throw new NotImplementedException();
    }
}