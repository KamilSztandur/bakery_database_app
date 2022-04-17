using System.Text;
using BakeryManager.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BakeryManager.WebApp.Controllers;

public class BakeriesController : Controller
{
    public IConfiguration Configuration;
    
    public BakeriesController(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public async Task<IActionResult> Index()
    {
        var restPath = GetHostUrl().Content + Cn();
        
        List<BakeryVM> bakerysList;

        using (var httpBakery = new HttpClient())
        {
            using (var response = await httpBakery.GetAsync(restPath))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                bakerysList = JsonConvert.DeserializeObject<List<BakeryVM>>(apiResponse) ?? new List<BakeryVM>();
            }
        }

        return View(bakerysList);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(BakeryVM bakery)
    {
        var restPath = GetHostUrl().Content + Cn();

        using (var httpBakery = new HttpClient())
        {
            var jsonString = JsonConvert.SerializeObject(bakery);
            
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpBakery.PostAsync(restPath, content);
            
            return RedirectToAction(nameof(Index));
        }
    }
    
    public async Task<IActionResult> Edit(string bakeryCode)
    {
        var restPath = GetHostUrl().Content + Cn();

        BakeryVM bakery;

        using (var httpBakery = new HttpClient())
        {
            using (var response = await httpBakery.GetAsync($"{restPath}/{bakeryCode}"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                bakery = JsonConvert.DeserializeObject<BakeryVM>(apiResponse) ?? new BakeryVM();

            }
        }

        return View(bakery);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BakeryVM bakery)
    {
        var restPath = GetHostUrl().Content + Cn();

        try
        {
            using (var httpBakery = new HttpClient())
            {
                var jsonString = JsonConvert.SerializeObject(bakery);
                
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                using (var response = await httpBakery.PutAsync($"{restPath}/{bakery.BakeryCode}", content))
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

    public async Task<IActionResult> Delete(string bakeryCode)
    {
        var restPath = GetHostUrl().Content + Cn();

        BakeryVM? bakery;

        using (var httpBakery = new HttpClient())
        {
            using (var response = await httpBakery.GetAsync($"{restPath}/{bakeryCode}"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                bakery = JsonConvert.DeserializeObject<BakeryVM>(apiResponse);
            }
        }

        return View(bakery);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(BakeryVM bakery)
    {
        var restPath = GetHostUrl().Content + Cn();

        try
        {
            using (var httpBakery = new HttpClient())
            {
                await httpBakery.DeleteAsync($"{restPath}/{bakery.BakeryCode}");
                
                Console.WriteLine($"{restPath}/{bakery.BakeryCode}");

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