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
        try
        {
            var editedDiscount = _appDbContext!.Discounts.FirstOrDefault(d => d.Id == id);
            if (editedDiscount == null)
            {
                return 404;
            }
            else
            {
                editedDiscount.Description = discount.Description;
                editedDiscount.IsActive = discount.IsActive;
                editedDiscount.MoneyThreshold = discount.MoneyThreshold;
                editedDiscount.ValueInPercents = discount.ValueInPercents;

                var result = await _appDbContext.SaveChangesAsync();
                await Task.CompletedTask;

                return result;   
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return -1;
        }
    }

    public async Task<int> DelAsync(int id)
    {
        try
        {
            var discountToRemove = _appDbContext!.Discounts.FirstOrDefault(discount => discount.Id == id);
            if (discountToRemove == null)
            {
                return 200;
            }
            else
            {
                _appDbContext!.Remove(discountToRemove);
                var result = await _appDbContext.SaveChangesAsync();

                await Task.CompletedTask;
                return result;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return -1;
        }
    }

    public async Task<int> AddAsync(Discount discount)
    {
        try
        {
            _appDbContext!.Discounts.Add(discount);
            var result = _appDbContext.SaveChanges();

            await Task.CompletedTask;
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return -1;
        }
    }

    public async Task<Discount?> GetAsync(int id)
    {
        try
        {
            var discount = await Task.FromResult(
                _appDbContext!.Discounts.FirstOrDefault(discount => discount.Id == id)
            );

            return await Task.FromResult(discount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<IEnumerable<Discount>> BrowseAllAsync()
    {
        try
        {
            IEnumerable<Discount> discounts = await Task.FromResult(_appDbContext!.Discounts);

            return discounts;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<Discount>();
        }
    }
}