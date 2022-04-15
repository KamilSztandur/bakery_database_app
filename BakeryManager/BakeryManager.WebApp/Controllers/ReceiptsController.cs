using System.Text;
using BakeryManager.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BakeryManager.WebApp.Controllers;

public class ReceiptsController : Controller
{
    public IConfiguration Configuration;
    
    public ReceiptsController(IConfiguration configuration)
    {
        Configuration = configuration;
        
    }
    
    public async Task<IActionResult> Index()
    {
        var restPath = GetHostUrl().Content + Cn();
        
        List<ReceiptVM> receiptsList;

        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(restPath))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                receiptsList = JsonConvert.DeserializeObject<List<ReceiptVM>>(apiResponse) ?? new List<ReceiptVM>();
            }
        }

        return View(receiptsList);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(ReceiptVM receipt)
    {
        var restPath = GetHostUrl().Content + Cn();

        using (var httpClient = new HttpClient())
        {
            var jsonString = JsonConvert.SerializeObject(receipt);
            
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PostAsync(restPath, content);
            
            return RedirectToAction(nameof(Index));
        }
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var restPath = GetHostUrl().Content + Cn();

        ReceiptVM receipt;

        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync($"{restPath}/{id}"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                receipt = JsonConvert.DeserializeObject<ReceiptVM>(apiResponse) ?? new ReceiptVM();

            }
        }

        return View(receipt);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ReceiptVM receipt)
    {
        var restPath = GetHostUrl().Content + Cn();

        try
        {
            using (var httpClient = new HttpClient())
            {
                var jsonString = JsonConvert.SerializeObject(receipt);
                
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync($"{restPath}/{receipt.Id}", content))
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

        ReceiptVM? receipt;

        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync($"{restPath}/{id}"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                
                receipt = JsonConvert.DeserializeObject<ReceiptVM>(apiResponse);

            }
        }

        return View(receipt);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(ReceiptVM receipt)
    {
        var restPath = GetHostUrl().Content + Cn();

        try
        {
            using (var httpClient = new HttpClient())
            {
                await httpClient.DeleteAsync($"{restPath}/{receipt.Id}");

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