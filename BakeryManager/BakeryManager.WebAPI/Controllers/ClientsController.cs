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
    public async Task<string> BrowseAll()
    {
        var result = await _clientService.BrowseAll();

        var resultAsJson = Newtonsoft.Json.JsonConvert.SerializeObject(result);
        return resultAsJson;
    }

    [HttpGet("{id}")]
    public async Task<string> GetClient(int id)
    {
       var result = await _clientService.GetClient(id);

        var resultAsJson = Newtonsoft.Json.JsonConvert.SerializeObject(result);
        return resultAsJson;
    }

    [HttpPost]
    public async Task<string> AddClient([FromBody] CreateClient _client)
    {
        var result = await _clientService.AddClient(_client);
        
        var resultAsJson = Newtonsoft.Json.JsonConvert.SerializeObject(result);
        return resultAsJson;
    }

    [HttpPut("{id}")]
    public async Task<string> UpdateClient([FromBody] CreateClient _client, int id)
    {
        var result = await _clientService.UpdateClient(id, _client);

        var resultAsJson = Newtonsoft.Json.JsonConvert.SerializeObject(result);
        return resultAsJson;
    }

    [HttpDelete("{id}")]
    public async Task<string> DeleteClient(int id)
    {
        var result = await _clientService.DeleteClient(id);
        var resultAsJson = Newtonsoft.Json.JsonConvert.SerializeObject(result);
        
        return resultAsJson;
    }
}
