using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BakeryManager.WebAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class ReceiptsController : Controller
{
    private readonly IReceiptService _receiptService;
    
    public ReceiptsController(IReceiptService receiptService)
    {
        _receiptService = receiptService;
    }

    [HttpGet]
    public async Task<IActionResult> BrowseAll()
    {
        var result = await _receiptService.BrowseAll();

        return Json(result);;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReceipt(int id)
    {
        var result = await _receiptService.GetReceipt(id);

        if (result == null)
        {
            return NotFound();
        }
       
        return Json(result);;
    }

    [HttpPost]
    public async Task<IActionResult> AddReceipt([FromBody] CreateReceipt receipt)
    {
        var result = await _receiptService.AddReceipt(receipt);

        if (result == -1)
        {
            return BadRequest("Invalid Receipt body.");
        }
        
        return Json(result);;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReceipt([FromBody] CreateReceipt receipt, int id)
    {
        var result = await _receiptService.UpdateReceipt(id, receipt);
        
        switch (result)
        {
            case -1:
                return BadRequest("Invalid Receipt body.");
            case 404:
                return NotFound();
            default:
                return Ok();;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReceipt(int id)
    {
        var result = await _receiptService.DeleteReceipt(id);
        
        if (result == -1)
        {
            return NotFound();
        }
        
        return Ok();
    }
}