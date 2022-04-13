using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BakeryManager.WebAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class BakeriesController : Controller
{
    private readonly IBakeryService _bakeryService;
    
    public BakeriesController(IBakeryService bakeryService)
    {
        _bakeryService = bakeryService;
    }

    [HttpGet]
    public async Task<IActionResult> BrowseAll()
    {
        var result = await _bakeryService.BrowseAll();

        return Ok(Json(result));
    }

    [HttpGet("{bakeryCode}")]
    public async Task<IActionResult> GetBakery(string bakeryCode)
    {
        var result = await _bakeryService.GetBakery(bakeryCode);

        if (result == null)
        {
            return NotFound();
        }
       
        return Ok(Json(result));
    }

    [HttpPost]
    public async Task<IActionResult> AddBakery([FromBody] CreateBakery bakery)
    {
        var result = await _bakeryService.AddBakery(bakery);

        if (result == -1)
        {
            return BadRequest("InvalbakeryCode Bakery body.");
        }
        
        return Ok(Json(result));
    }

    [HttpPut("{bakeryCode}")]
    public async Task<IActionResult> UpdateBakery([FromBody] CreateBakery bakery, string bakeryCode)
    {
        var result = await _bakeryService.UpdateBakery(bakeryCode, bakery);
        
        switch (result)
        {
            case -1:
                return BadRequest("InvalbakeryCode Bakery body.");
            case 404:
                return NotFound();
            default:
                return Ok();;
        }
    }

    [HttpDelete("{bakeryCode}")]
    public async Task<IActionResult> DeleteBakery(string bakeryCode)
    {
        var result = await _bakeryService.DeleteBakery(bakeryCode);
        
        if (result == -1)
        {
            return NotFound();
        }
        
        return Ok();
    }
}