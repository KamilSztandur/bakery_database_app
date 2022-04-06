using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.DTO;

namespace BakeryManager.Infrastructure.Services.Interfaces;

public interface IReceiptService
{
    Task<int> UpdateReceipt(int id, CreateReceipt receiptBody);
    Task<int> DeleteReceipt(int id);
    Task<int> AddReceipt(CreateReceipt receiptBody);
    Task<ReceiptDTO> GetReceipt(int id);
    Task<IEnumerable<ReceiptDTO>> BrowseAll();
}