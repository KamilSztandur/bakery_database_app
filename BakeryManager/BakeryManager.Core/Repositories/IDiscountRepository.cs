using BakeryManager.Core.Domain;

namespace BakeryManager.Core.Repositories;

public interface IDiscountRepository
{
    Task<int> UpdateAsync(int id, Discount discount);

    Task<int> DelAsync(int id);

    Task<int> AddAsync(Discount discount);

    Task<Discount?> GetAsync(int id);

    Task<IEnumerable<Discount>> BrowseAllAsync();
}