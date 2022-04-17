using BakeryManager.Core.Domain;
using BakeryManager.Core.Repositories;
using System.Linq;

namespace BakeryManager.Infrastructure.Repositories;

public class ReceiptRepository : IReceiptRepository
{
    private readonly AppDbContext? _appDbContext;

    public ReceiptRepository(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }
    
    public async Task<int> UpdateAsync(int id, Receipt receipt)
    {
        try
        {
            var editedReceipt = _appDbContext!.Receipts.FirstOrDefault(d => d.Id == id);
            if (editedReceipt == null)
            {
                return 404;
            }
            else
            {
                editedReceipt.Date = receipt.Date;
                editedReceipt.BakeryCode = receipt.BakeryCode;
                editedReceipt.ClientId = receipt.ClientId;
                editedReceipt.ProductId = receipt.ProductId;
                editedReceipt.TotalPrice = receipt.TotalPrice;

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

    public async Task<int> DelAsync(int id)
    {
        try
        {
            var receiptToRemove = _appDbContext!.Receipts.FirstOrDefault(receipt => receipt.Id == id);
            if (receiptToRemove == null)
            {
                return 200;
            }
            else
            {
                _appDbContext!.Remove(receiptToRemove);
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

    public async Task<int> AddAsync(Receipt receipt)
    {

        try
        {
            var bakeryExists = 
                (from bakery in _appDbContext!.Bakeries
                    where bakery.BakeryCode == receipt.BakeryCode
                    select bakery).Count() == 1;
            
            var clientExists = 
                (from client in _appDbContext!.Clients 
                    where client.Id == receipt.ClientId 
                    select client).Count() == 1;

            var productExists =
                (from product in _appDbContext!.Products
                    where product.Id == receipt.ProductId
                    select product).Count() == 1;

            if (!bakeryExists || !clientExists || !productExists)
            {
                return -2;
            }
        } catch (Exception)
        {
            return -2;
        }
        
        try
        {
            _appDbContext!.Receipts.Add(receipt);
            var result = await _appDbContext.SaveChangesAsync();

            await Task.CompletedTask;
            return result;
        }
        catch (Exception e)
        {
            return -1;
        }
    }

    public async Task<Receipt?> GetAsync(int id)
    {
        try
        {
            var receipt = await Task.FromResult(
                _appDbContext!.Receipts.FirstOrDefault(receipt => receipt.Id == id)
            );

            return await Task.FromResult(receipt);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<IEnumerable<Receipt>> BrowseAllAsync()
    {
        try
        {
            IEnumerable<Receipt> receipts = await Task.FromResult(_appDbContext!.Receipts);

            return receipts;
        }
        catch (Exception e)
        {
            return new List<Receipt>();
        }
    }
}