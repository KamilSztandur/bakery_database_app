using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.DTO;

namespace BakeryManager.Infrastructure.Services.Interfaces;

public interface IProductService
{
    Task<int> UpdateProduct(int id, CreateProduct productBody);
    Task<int> DeleteProduct(int id);
    Task<int> AddProduct(CreateProduct productBody);
    Task<ProductDTO?> GetProduct(int id);
    Task<IEnumerable<ProductDTO>> BrowseAll();
}