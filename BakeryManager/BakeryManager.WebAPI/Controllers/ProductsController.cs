using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BakeryManager.WebAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> BrowseAll()
    {
        var result = await _productService.BrowseAll();

        return Json(result);;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var result = await _productService.GetProduct(id);

        if (result == null)
        {
            return NotFound();
        }
       
        return Json(result);;
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] CreateProduct product)
    {
        var result = await _productService.AddProduct(product);

        if (result == -1)
        {
            return BadRequest("Invalid Product body.");
        }
        
        return Json(result);;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromBody] CreateProduct product, int id)
    {
        var result = await _productService.UpdateProduct(id, product);
        
        switch (result)
        {
            case -1:
                return BadRequest("Invalid Product body.");
            case 404:
                return NotFound();
            default:
                return Ok();;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _productService.DeleteProduct(id);
        
        if (result == -1)
        {
            return NotFound();
        }
        
        return Ok();
    }
}