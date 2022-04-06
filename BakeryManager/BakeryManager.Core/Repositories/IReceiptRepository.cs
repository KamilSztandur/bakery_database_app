using BakeryManager.Core.Domain;

namespace BakeryManager.Core.Repositories;

public interface IReceiptRepository
{
    Task<int> UpdateAsync(int id, Receipt receipt);

    Task<int> DelAsync(int id);

    Task<int> AddAsync(Receipt receipt);

    Task<Receipt> GetAsync(int id);

    Task<IEnumerable<Receipt>> BrowseAllAsync();
}