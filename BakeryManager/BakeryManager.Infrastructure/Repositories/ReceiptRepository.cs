using BakeryManager.Core.Domain;
using BakeryManager.Core.Repositories;

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
            Console.WriteLine(e.Message);
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
            Console.WriteLine(e.Message);
            return -1;
        }
    }

    public async Task<int> AddAsync(Receipt receipt)
    {
        try
        {
            _appDbContext!.Receipts.Add(receipt);
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
            Console.WriteLine(e.Message);
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
            Console.WriteLine(e.Message);
            return new List<Receipt>();
        }
    }
}