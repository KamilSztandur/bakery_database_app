using BakeryManager.Core.Domain;
using BakeryManager.Core.Repositories;

namespace BakeryManager.Infrastructure.Repositories;

public class BakeryRepository : IBakeryRepository
{
    private readonly AppDbContext? _appDbContext;

    public BakeryRepository(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }
    
    public async Task<int> UpdateAsync(string bakeryCode, Bakery bakery)
    {
        try
        {
            var editedBakery = _appDbContext!.Bakeries.FirstOrDefault(b => b.BakeryCode == bakeryCode);
            if (editedBakery == null)
            {
                return 404;
            }
            else
            {
                editedBakery.PostalCode = bakery.PostalCode;
                editedBakery.StreetName = bakery.StreetName;
                editedBakery.StreetNumber = bakery.StreetNumber;
                editedBakery.TownName = bakery.TownName;

                var result = await _appDbContext.SaveChangesAsync();
                await Task.CompletedTask;

                return result;   
            }
        }
        catch (Exception e)
        {
            return -1;
        }
    }

    public async Task<int> DelAsync(string bakeryCode)
    {
        try
        {
            var bakeryToRemove = _appDbContext!.Bakeries.FirstOrDefault(bakery => bakery.BakeryCode == bakeryCode);
            if (bakeryToRemove == null)
            {
                return 200;
            }
            else
            {
                _appDbContext!.Remove(bakeryToRemove);
                var result = await _appDbContext.SaveChangesAsync();

                await Task.CompletedTask;
                return result;
            }
        }
        catch (Exception e)
        {
            return -1;
        }
    }

    public async Task<int> AddAsync(Bakery bakery)
    {
        try
        {
            _appDbContext!.Bakeries.Add(bakery);
            var result = await _appDbContext.SaveChangesAsync();

            await Task.CompletedTask;
            return result;
        }
        catch (Exception e)
        {
            return -1;
        }
    }

    public async Task<Bakery?> GetAsync(string bakeryCode)
    {
        try
        {
            var bakery = _appDbContext!.Bakeries.FirstOrDefault(bakery => bakery.BakeryCode == bakeryCode);
            await Task.CompletedTask;
            
            return bakery;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<IEnumerable<Bakery>> BrowseAllAsync()
    {
        try
        {
            var bakeries = (await Task.FromResult(_appDbContext!.Bakeries)) as IEnumerable<Bakery>;

            return bakeries;
        }
        catch (Exception e)
        {
            return new List<Bakery>();
        }
    }
}