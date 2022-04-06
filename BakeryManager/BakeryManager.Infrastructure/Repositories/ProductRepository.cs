using BakeryManager.Core.Domain;
using BakeryManager.Core.Repositories;

namespace BakeryManager.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext? _appDbContext;

    public ProductRepository(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }
    
    public async Task<int> UpdateAsync(int id, Product product)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DelAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Product>> BrowseAllAsync()
    {
        throw new NotImplementedException();
    }
}