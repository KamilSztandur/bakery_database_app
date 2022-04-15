using System.Text;
using BakeryManager.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BakeryManager.WebApp.Controllers;

public class ClientsController : Controller
{
    public IConfiguration Configuration;
    
    public ClientsController(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public async Task<IActionResult> Index()
    {
        var restPath = GetHostUrl().Content + Cn();
        
        List<ClientVM> clientsList;

        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(restPath))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                clientsList = JsonConvert.DeserializeObject<List<ClientVM>>(apiResponse) ?? new List<ClientVM>();
            }
        }

        return View(clientsList);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(ClientVM client)
    {
        var restPath = GetHostUrl().Content + Cn();

        using (var httpClient = new HttpClient())
        {
            var jsonString = JsonConvert.SerializeObject(client);
            
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PostAsync(restPath, content);
            
            return RedirectToAction(nameof(Index));
        }
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var restPath = GetHostUrl().Content + Cn();

        ClientVM client;

        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync($"{restPath}/{id}"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                client = JsonConvert.DeserializeObject<ClientVM>(apiResponse) ?? new ClientVM();

            }
        }

        return View(client);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ClientVM client)
    {
        var restPath = GetHostUrl().Content + Cn();

        try
        {
            using (var httpClient = new HttpClient())
            {
                var jsonString = JsonConvert.SerializeObject(client);
                
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync($"{restPath}/{client.Id}", content))
                {
                    await response.Content.ReadAsStringAsync();
                    
                    return RedirectToAction(nameof(Index));
                }
            }
        }
        catch (Exception ex)
        {
            return View(null);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var restPath = GetHostUrl().Content + Cn();

        ClientVM? client;

        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync($"{restPath}/{id}"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                
                client = JsonConvert.DeserializeObject<ClientVM>(apiResponse);

            }
        }

        return View(client);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(ClientVM client)
    {
        var restPath = GetHostUrl().Content + Cn();

        try
        {
            using (var httpClient = new HttpClient())
            {
                await httpClient.DeleteAsync($"{restPath}/{client.Id}");

                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception)
        {
            return RedirectToAction(nameof(Index));
        }
    }

    
    private ContentResult GetHostUrl()
    {
        var result = Configuration["RestApiUrl:HostUrl"];
        
        return Content(result);
    }

    private string Cn()
    {
        var cn = ControllerContext.RouteData.Values["controller"]!.ToString()!;
        return cn;
    }
}