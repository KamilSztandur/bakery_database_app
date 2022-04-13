using BakeryManager.Core.Domain;
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
        if (!IsBodyValid(discountBody))
        {
            return await Task.FromResult(-1);
        }
        
        var discount = ParseCreateDiscountIntoDiscount(discountBody);
        var result = await _discountsRepository.UpdateAsync(id, discount);
        
        return result;
    }

    public async Task<int> DeleteDiscount(int id)
    {
        var result = await _discountsRepository.DelAsync(id);
        
        return result;
    }

    public async Task<int> AddDiscount(CreateDiscount discountBody)
    {
        if (!IsBodyValid(discountBody))
        {
            return -1;
        }
        
        var discount = ParseCreateDiscountIntoDiscount(discountBody);
        var result = await _discountsRepository.AddAsync(discount);

        return result;
    }

    public async Task<DiscountDTO?> GetDiscount(int id)
    {
        var discount = await _discountsRepository.GetAsync(id);

        if (discount == null)
        {
            return null;
        }
        else
        {
            return ParseDiscountIntoDiscountDTO(discount);
        }
    }

    public async Task<IEnumerable<DiscountDTO>> BrowseAll()
    {
        var discounts = await _discountsRepository.BrowseAllAsync();

        var discountsDTOs = discounts.Select(discount => ParseDiscountIntoDiscountDTO(discount));

        return discountsDTOs;
    }

    private static bool IsBodyValid(CreateDiscount body) => body.ValueInPercents <= 1;
    
    private DiscountDTO ParseDiscountIntoDiscountDTO(Discount discount)
    {
        return new DiscountDTO()
        {
            Id = discount.Id,
            MoneyThreshold = discount.MoneyThreshold,
            ValueInPercents = discount.ValueInPercents,
            IsActive = discount.IsActive,
            Description = discount.Description
        };
    }

    private Discount ParseCreateDiscountIntoDiscount(CreateDiscount discountBody)
    {
        return new Discount
        {
            MoneyThreshold = discountBody.MoneyThreshold,
            ValueInPercents = discountBody.ValueInPercents,
            IsActive = discountBody.IsActive,
            Description = discountBody.Description
        };
    }
}