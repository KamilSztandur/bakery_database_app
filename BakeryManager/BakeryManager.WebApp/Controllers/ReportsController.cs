using BakeryManager.WebApp.Common;
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
    
    [HttpGet]
    [Route("/reports/download/premium-clients")]
    public async Task<FileStreamResult> DownloadPremiumClients()
    {
        List<PremiumClientVM>? premiumClientsList;

        using (var httpClient = new HttpClient())
        {
            var restPath = GetHostUrl().Content + "views/";
            
            using (var response = await httpClient.GetAsync(restPath + "premium-clients"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                premiumClientsList = JsonConvert.DeserializeObject<List<PremiumClientVM>>(apiResponse);
            }
        }

        var fileStream = ReportGenerator.GetPremiumClientsAsCsv(premiumClientsList ?? new List<PremiumClientVM>());
        var filename = GetCsvFileName("Premium Clients");
        var file = File(fileStream!, "text/csv", filename);

        return file;
    }
    
    [HttpGet]
    [Route("/reports/download/bakeries-earnings")]
    public async Task<FileStreamResult> DownloadBakeriesEarnings()
    {
        List<BakeryEarningsVM>? bakeryEarningsList;

        using (var httpClient = new HttpClient())
        {
            var restPath = GetHostUrl().Content + "views/";
            
            using (var response = await httpClient.GetAsync(restPath + "bakeries-earnings"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                bakeryEarningsList = JsonConvert.DeserializeObject<List<BakeryEarningsVM>>(apiResponse);
            }
        }

        var fileStream = ReportGenerator.GetBakeriesEarningsAsCsv(bakeryEarningsList ?? new List<BakeryEarningsVM>());
        var filename = GetCsvFileName("Bakeries Earnings");
        var file = File(fileStream!, "text/csv", filename);

        return file;
    }

    [HttpGet]
    [Route("/reports/download/earnings-per-product")]
    public async Task<FileStreamResult> DownloadEarningsPerProduct()
    {
        List<EarningsPerProductVM>? earningsPerProductList;

        using (var httpClient = new HttpClient())
        {
            var restPath = GetHostUrl().Content + "views/";
            
            using (var response = await httpClient.GetAsync(restPath + "earnings-per-product"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                earningsPerProductList = JsonConvert.DeserializeObject<List<EarningsPerProductVM>>(apiResponse);
            }
        }

        var fileStream = ReportGenerator.GetEarningsPerProductAsCsv(earningsPerProductList ?? new List<EarningsPerProductVM>());
        var filename = GetCsvFileName("Earnings Per Product");
        var file = File(fileStream!, "text/csv", filename);

        return file;
    }
    
    [HttpGet]
    [Route("/reports/download/sold-products")]
    public async Task<FileStreamResult> DownloadSoldProducts()
    {
        List<SoldProductVM>? soldProductsList;

        using (var httpClient = new HttpClient())
        {
            var restPath = GetHostUrl().Content + "views/";
            
            using (var response = await httpClient.GetAsync(restPath + "sold-products"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                soldProductsList = JsonConvert.DeserializeObject<List<SoldProductVM>>(apiResponse);
            }
        }

        var fileStream = ReportGenerator.GetSoldProductsAsCsv(soldProductsList ?? new List<SoldProductVM>());
        var filename = GetCsvFileName("Sold Products");
        var file = File(fileStream!, "text/csv", filename);

        return file;
    }
    
    [HttpGet]
    [Route("/reports/download/clients-expenses")]
    public async Task<FileStreamResult> DownloadClientsExpenses()
    {
        List<ClientExpensesVM>? clientsExpensesList;

        using (var httpClient = new HttpClient())
        {
            var restPath = GetHostUrl().Content + "views/";
            
            using (var response = await httpClient.GetAsync(restPath + "clients-expenses"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                clientsExpensesList = JsonConvert.DeserializeObject<List<ClientExpensesVM>>(apiResponse);
            }
        }

        var fileStream = ReportGenerator.GetClientsExpensesAsCsv(clientsExpensesList ?? new List<ClientExpensesVM>());
        var filename = GetCsvFileName("Clients Expenses");
        var file = File(fileStream!, "text/csv", filename);

        return file;
    }

    private static string GetCsvFileName(string title)
    {
        return title.Replace(" ", "-").ToLower() + "-" + DateTime.Now.ToString("dd-MM-yyyy") + "-report.csv";
    }

    private ContentResult GetHostUrl()
    {
        var result = Configuration["RestApiUrl:HostUrl"];
        
        return Content(result);
    }
}
