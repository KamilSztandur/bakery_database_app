using BakeryManager.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BakeryManager.WebAPI.Controllers;

public class ViewsController : Controller
{
    private readonly IViewsService _viewsService;
    
    public ViewsController(IViewsService viewsService)
    {
        _viewsService = viewsService;
    }

    [HttpGet]
    [Route("/views/bakeries-earnings")]
    public async Task<IActionResult> BrowseAllBakeriesEarnings()
    {
        var result = await _viewsService.GetBakeryEarnings();

        return Ok(Json(result));
    }
    
    [HttpGet]
    [Route("/views/clients-expenses")]
    public async Task<IActionResult> BrowseAllClientsExpenses()
    {
        var result = await _viewsService.GetClientsExpenses();

        return Ok(Json(result));
    }
    
    [HttpGet]
    [Route("/views/earnings-per-product")]
    public async Task<IActionResult> BrowseAllEarningsPerProduct()
    {
        var result = await _viewsService.GetEarningsPerProduct();

        return Ok(Json(result));
    }
    
    [HttpGet]
    [Route("/views/premium-clients")]
    public async Task<IActionResult> BrowseAllPremiumClients()
    {
        var result = await _viewsService.GetPremiumClients();

        return Ok(Json(result));
    }
    
    [HttpGet]
    [Route("/views/sold-products")]
    public async Task<IActionResult> BrowseAll()
    {
        var result = await _viewsService.GetSoldProducts();

        return Ok(Json(result));
    }
}