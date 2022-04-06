using BakeryManager.Core.Repositories;
using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.DTO;
using BakeryManager.Infrastructure.Services.Interfaces;

namespace BakeryManager.Infrastructure.Services;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _discountsRepository;
    
    public DiscountService(IDiscountRepository discountsRepository)
    {
        _discountsRepository = discountsRepository;
    }
    
    public async Task<int> UpdateDiscount(int id, CreateDiscount discountBody)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteDiscount(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddDiscount(CreateDiscount discountBody)
    {
        throw new NotImplementedException();
    }

    public async Task<DiscountDTO> GetDiscount(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<DiscountDTO>> BrowseAll()
    {
        throw new NotImplementedException();
    }
}