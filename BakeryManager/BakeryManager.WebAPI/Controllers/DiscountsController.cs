using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BakeryManager.WebAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class DiscountsController : Controller
{
    private readonly IDiscountService _discountService;
    
    public DiscountsController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [HttpGet]
    public async Task<IActionResult> BrowseAll()
    {
        var result = await _discountService.BrowseAll();

        return Ok(Json(result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDiscount(int id)
    {
        var result = await _discountService.GetDiscount(id);

        if (result == null)
        {
            return NotFound();
        }
       
        return Ok(Json(result));
    }

    [HttpPost]
    public async Task<IActionResult> AddDiscount([FromBody] CreateDiscount discount)
    {
        var result = await _discountService.AddDiscount(discount);

        if (result == -1)
        {
            return BadRequest("Invalid Discount body.");
        }
        
        return Ok(Json(result));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDiscount([FromBody] CreateDiscount discount, int id)
    {
        var result = await _discountService.UpdateDiscount(id, discount);
        
        switch (result)
        {
            case -1:
                return BadRequest("Invalid Discount body.");
            case 404:
                return NotFound();
            default:
                return Ok();;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiscount(int id)
    {
        var result = await _discountService.DeleteDiscount(id);
        
        if (result == -1)
        {
            return NotFound();
        }
        
        return Ok();
    }
}