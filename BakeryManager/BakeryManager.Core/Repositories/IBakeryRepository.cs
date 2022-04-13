using BakeryManager.Core.Domain;

namespace BakeryManager.Core.Repositories;

public interface IBakeryRepository
{
    Task<int> UpdateAsync(String bakeryCode, Bakery bakery);

    Task<int> DelAsync(String bakeryCode);

    Task<int> AddAsync(Bakery bakery);

    Task<Bakery?> GetAsync(String bakeryCode);

    Task<IEnumerable<Bakery>> BrowseAllAsync();
}