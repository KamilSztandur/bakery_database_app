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
        throw new NotImplementedException();
    }

    public async Task<int> DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddProduct(CreateProduct productBody)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductDTO> GetProduct(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProductDTO>> BrowseAll()
    {
        throw new NotImplementedException();
    }
}