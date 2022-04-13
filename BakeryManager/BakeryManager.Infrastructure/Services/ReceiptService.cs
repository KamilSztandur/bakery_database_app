using BakeryManager.Core.Domain;
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
        if (!IsBodyValid(receiptBody))
        {
            return await Task.FromResult(-1);
        }
        
        var receipt = ParseCreateReceiptIntoReceipt(receiptBody);
        var result = await _receiptsRepository.UpdateAsync(id, receipt);
        
        return result;
    }

    public async Task<int> DeleteReceipt(int id)
    {
        var result = await _receiptsRepository.DelAsync(id);
        
        return result;
    }

    public async Task<int> AddReceipt(CreateReceipt receiptBody)
    {
        if (!IsBodyValid(receiptBody))
        {
            return -1;
        }
        
        var receipt = ParseCreateReceiptIntoReceipt(receiptBody);
        var result = await _receiptsRepository.AddAsync(receipt);

        return result;
    }

    public async Task<ReceiptDTO?> GetReceipt(int id)
    {
        var receipt = await _receiptsRepository.GetAsync(id);

        if (receipt == null)
        {
            return null;
        }
        else
        {
            return ParseReceiptIntoReceiptDTO(receipt);
        }
    }

    public async Task<IEnumerable<ReceiptDTO>> BrowseAll()
    {
        var receipts = await _receiptsRepository.BrowseAllAsync();

        var receiptsDTOs = receipts.Select(receipt => ParseReceiptIntoReceiptDTO(receipt));

        return receiptsDTOs;
    }


    private static bool IsBodyValid(CreateReceipt body)
    {
        if (body.Date == null || body.BakeryCode == null)
        {
            return false;
        }
        
        //TODO Add check for invalid clients, bakeries and products IDs
        
        return true;
    }
    
    private ReceiptDTO ParseReceiptIntoReceiptDTO(Receipt receipt)
    {
        return new ReceiptDTO
        {
            Id = receipt.Id,
            ClientId = receipt.ClientId,
            ProductId = receipt.ProductId,
            BakeryCode = receipt.BakeryCode,
            TotalPrice = receipt.TotalPrice,
            Date = receipt.Date,
        };
    }

    private Receipt ParseCreateReceiptIntoReceipt(CreateReceipt receiptBody)
    {
        return new Receipt()
        {
            ClientId = receiptBody.ClientId,
            ProductId = receiptBody.ProductId,
            BakeryCode = receiptBody.BakeryCode!,
            TotalPrice = receiptBody.TotalPrice,
            Date = DateTime.Parse(receiptBody.Date!),
        };
    }
}