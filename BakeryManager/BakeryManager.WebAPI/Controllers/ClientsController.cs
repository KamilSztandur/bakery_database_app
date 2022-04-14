using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BakeryManager.WebAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class ClientsController : Controller
{
    private readonly IClientService _clientService;
    
    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> BrowseAll()
    {
        var result = await _clientService.BrowseAll();

        return Json(result);;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClient(int id)
    {
       var result = await _clientService.GetClient(id);

       if (result == null)
       {
           return NotFound();
       }
       
       return Json(result);;
    }

    [HttpPost]
    public async Task<IActionResult> AddClient([FromBody] CreateClient client)
    {
        var result = await _clientService.AddClient(client);

        if (result == -1)
        {
            return BadRequest("Invalid Client body.");
        }
        
        return Json(result);;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient([FromBody] CreateClient client, int id)
    {
        var result = await _clientService.UpdateClient(id, client);
        
        switch (result)
        {
            case -1:
                return BadRequest("Invalid Client body.");
            case 404:
                return NotFound();
            default:
                return Ok();;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var result = await _clientService.DeleteClient(id);
        
        if (result == -1)
        {
            return NotFound();
        }
        
        return Ok();
    }
}
