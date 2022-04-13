using BakeryManager.Core.Domain;
using BakeryManager.Core.Repositories;
using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.DTO;
using BakeryManager.Infrastructure.Services.Interfaces;

namespace BakeryManager.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productsRepository;
    
    public ProductService(IProductRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }
    
    public async Task<int> UpdateProduct(int id, CreateProduct productBody)
    {
        if (!IsBodyValid(productBody))
        {
            return await Task.FromResult(-1);
        }
        
        var product = ParseCreateProductIntoProduct(productBody);
        var result = await _productsRepository.UpdateAsync(id, product);
        
        return result;
    }

    public async Task<int> DeleteProduct(int id)
    {
        var result = await _productsRepository.DelAsync(id);
        
        return result;
    }

    public async Task<int> AddProduct(CreateProduct productBody)
    {
        if (!IsBodyValid(productBody))
        {
            return -1;
        }
        
        var product = ParseCreateProductIntoProduct(productBody);
        var result = await _productsRepository.AddAsync(product);

        return result;
    }

    public async Task<ProductDTO?> GetProduct(int id)
    {
        var product = await _productsRepository.GetAsync(id);

        if (product == null)
        {
            return null;
        }
        else
        {
            return ParseProductIntoProductDTO(product);
        }
    }

    public async Task<IEnumerable<ProductDTO>> BrowseAll()
    {
        var products = await _productsRepository.BrowseAllAsync();

        var productsDTOs = products.Select(product => ParseProductIntoProductDTO(product));

        return productsDTOs;
    }

    private static bool IsBodyValid(CreateProduct body) => body.Name != null;
    
    private ProductDTO ParseProductIntoProductDTO(Product product)
    {
        return new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }

    private Product ParseCreateProductIntoProduct(CreateProduct productBody)
    {
        return new Product()
        {
            Name = productBody.Name!,
            Price = productBody.Price
        };
    }
}