using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.DTO;

namespace BakeryManager.Infrastructure.Services.Interfaces;

public interface IDiscountService
{   
    Task<int> UpdateDiscount(int id, CreateDiscount discountBody);
    Task<int> DeleteDiscount(int id);
    Task<int> AddDiscount(CreateDiscount discountBody);
    Task<DiscountDTO?> GetDiscount(int id);
    Task<IEnumerable<DiscountDTO>> BrowseAll();
}