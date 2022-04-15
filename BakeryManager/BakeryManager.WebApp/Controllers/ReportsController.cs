using BakeryManager.WebApp.Models.ReportsModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BakeryManager.WebApp.Controllers;

public class ReportsController : Controller
{
    public IConfiguration Configuration;
    
    public ReportsController(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public async Task<IActionResult> Index()
    {
        var restPath = GetHostUrl().Content + "views/";
        
        List<BakeryEarningsVM>? bakeriesEarningsList;
        List<ClientExpensesVM>? clientsExpensesList;
        List<EarningsPerProductVM>? earningsPerProductList;
        List<PremiumClientVM>? premiumClientsList;
        List<SoldProductVM>? soldProductsList;

        using (var httpClient = new HttpClient())
        {
            
            using (var response = await httpClient.GetAsync(restPath + "bakeries-earnings"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                bakeriesEarningsList = JsonConvert.DeserializeObject<List<BakeryEarningsVM>>(apiResponse);
            }
            
            using (var response = await httpClient.GetAsync(restPath + "clients-expenses"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                clientsExpensesList = JsonConvert.DeserializeObject<List<ClientExpensesVM>>(apiResponse);
            }
            
            using (var response = await httpClient.GetAsync(restPath + "earnings-per-product"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                earningsPerProductList = JsonConvert.DeserializeObject<List<EarningsPerProductVM>>(apiResponse);
            }
            
            

            using (var response = await httpClient.GetAsync(restPath + "premium-clients"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine("AAAAAAAAAA" + apiResponse + "B");

                premiumClientsList = JsonConvert.DeserializeObject<List<PremiumClientVM>>(apiResponse);
            }
            
            using (var response = await httpClient.GetAsync(restPath + "sold-products"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                soldProductsList= JsonConvert.DeserializeObject<List<SoldProductVM>>(apiResponse);
            }
        }

        var reports = new Reports
        {
            BakeriesEarnings = bakeriesEarningsList ?? new List<BakeryEarningsVM>(),
            ClientsExpenses = clientsExpensesList ?? new List<ClientExpensesVM>(),
            EarningsPerProduct = earningsPerProductList ?? new List<EarningsPerProductVM>(),
            PremiumClients = premiumClientsList ?? new List<PremiumClientVM>(),
            SoldProducts = soldProductsList ?? new List<SoldProductVM>()
        };

        return View(reports);
    }


    private ContentResult GetHostUrl()
    {
        var result = Configuration["RestApiUrl:HostUrl"];
        
        return Content(result);
    }
}
