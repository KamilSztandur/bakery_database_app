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
        
        return Json(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClient(int id)
    {
       var result = await _clientService.GetClient(id);

       return Json(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddClient([FromBody] CreateClient _client)
    {
        var result = await _clientService.AddClient(_client);
        
        return Json(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient([FromBody] CreateClient _client, int id)
    {
        var result = await _clientService.UpdateClient(id, _client);

        return Json(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var result = await _clientService.DeleteClient(id);
        
        return Json(result);
    }
}
