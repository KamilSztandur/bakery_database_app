using System.Text;
using BakeryManager.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BakeryManager.WebApp.Controllers;

public class DiscountsController : Controller
{
    public IConfiguration Configuration;
    
    public DiscountsController(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public async Task<IActionResult> Index()
    {
        var restPath = GetHostUrl().Content + Cn();
        
        List<DiscountVM> discountsList;

        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(restPath))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                discountsList = JsonConvert.DeserializeObject<List<DiscountVM>>(apiResponse) ?? new List<DiscountVM>();
            }
        }

        return View(discountsList);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(DiscountVM discount)
    {
        var restPath = GetHostUrl().Content + Cn();

        using (var httpClient = new HttpClient())
        {
            var jsonString = JsonConvert.SerializeObject(discount);
            
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PostAsync(restPath, content);
            
            return RedirectToAction(nameof(Index));
        }
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var restPath = GetHostUrl().Content + Cn();

        DiscountVM discount;

        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync($"{restPath}/{id}"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                discount = JsonConvert.DeserializeObject<DiscountVM>(apiResponse) ?? new DiscountVM();

            }
        }

        return View(discount);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DiscountVM discount)
    {
        var restPath = GetHostUrl().Content + Cn();

        try
        {
            using (var httpClient = new HttpClient())
            {
                var jsonString = JsonConvert.SerializeObject(discount);
                
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync($"{restPath}/{discount.Id}", content))
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

        DiscountVM? discount;

        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync($"{restPath}/{id}"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                
                discount = JsonConvert.DeserializeObject<DiscountVM>(apiResponse);

            }
        }

        return View(discount);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DiscountVM discount)
    {
        var restPath = GetHostUrl().Content + Cn();

        try
        {
            using (var httpClient = new HttpClient())
            {
                await httpClient.DeleteAsync($"{restPath}/{discount.Id}");

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