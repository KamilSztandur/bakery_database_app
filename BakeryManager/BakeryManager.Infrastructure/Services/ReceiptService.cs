using BakeryManager.Core.Repositories;
using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.DTO;
using BakeryManager.Infrastructure.Services.Interfaces;

namespace BakeryManager.Infrastructure.Services;

public class ReceiptService : IReceiptService
{
    private readonly IReceiptRepository _receiptsRepository;
    
    public ReceiptService(IReceiptRepository receiptsRepository)
    {
        _receiptsRepository = receiptsRepository;
    }

    public async Task<int> UpdateReceipt(int id, CreateReceipt receiptBody)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteReceipt(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddReceipt(CreateReceipt receiptBody)
    {
        throw new NotImplementedException();
    }

    public async Task<ReceiptDTO> GetReceipt(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ReceiptDTO>> BrowseAll()
    {
        throw new NotImplementedException();
    }
}