using BakeryManager.Core.Domain;

namespace BakeryManager.Core.Repositories;

public interface IProductRepository
{
    Task<int> UpdateAsync(int id, Product product);

    Task<int> DelAsync(int id);

    Task<int> AddAsync(Product product);

    Task<Product?> GetAsync(int id);

    Task<IEnumerable<Product>> BrowseAllAsync();
}