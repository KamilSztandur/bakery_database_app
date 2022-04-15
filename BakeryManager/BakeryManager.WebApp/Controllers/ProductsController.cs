using System.Text;
using BakeryManager.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BakeryManager.WebApp.Controllers;

public class ProductsController : Controller
{
    public IConfiguration Configuration;
    
    public ProductsController(IConfiguration configuration)
    {
        Configuration = configuration;
        
    }
    
    public async Task<IActionResult> Index()
    {
        var restPath = GetHostUrl().Content + Cn();
        
        List<ProductVM> productsList;

        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(restPath))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                productsList = JsonConvert.DeserializeObject<List<ProductVM>>(apiResponse) ?? new List<ProductVM>();
            }
        }

        return View(productsList);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(ProductVM product)
    {
        var restPath = GetHostUrl().Content + Cn();

        using (var httpClient = new HttpClient())
        {
            var jsonString = JsonConvert.SerializeObject(product);
            
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PostAsync(restPath, content);
            
            return RedirectToAction(nameof(Index));
        }
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var restPath = GetHostUrl().Content + Cn();

        ProductVM product;

        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync($"{restPath}/{id}"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<ProductVM>(apiResponse) ?? new ProductVM();

            }
        }

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductVM product)
    {
        var restPath = GetHostUrl().Content + Cn();

        try
        {
            using (var httpClient = new HttpClient())
            {
                var jsonString = JsonConvert.SerializeObject(product);
                
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync($"{restPath}/{product.Id}", content))
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

        ProductVM? product;

        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync($"{restPath}/{id}"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                
                product = JsonConvert.DeserializeObject<ProductVM>(apiResponse);

            }
        }

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(ProductVM product)
    {
        var restPath = GetHostUrl().Content + Cn();

        try
        {
            using (var httpClient = new HttpClient())
            {
                await httpClient.DeleteAsync($"{restPath}/{product.Id}");

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