using BakeryManager.Core.Domain;
using BakeryManager.Core.Repositories;

namespace BakeryManager.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext? _appDbContext;

    public ProductRepository(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }
    
    public async Task<int> UpdateAsync(int id, Product product)
    {
        try
        {
            var editedProduct = _appDbContext!.Products.FirstOrDefault(d => d.Id == id);
            if (editedProduct == null)
            {
                return 404;
            }
            else
            {
                editedProduct.Name = product.Name;
                editedProduct.Price = product.Price;

                var result = await _appDbContext.SaveChangesAsync();
                await Task.CompletedTask;

                return result;   
            }
        }
        catch (Exception e)
        {
            return -1;
        }
    }

    public async Task<int> DelAsync(int id)
    {
        try
        {
            var productToRemove = _appDbContext!.Products.FirstOrDefault(product => product.Id == id);
            if (productToRemove == null)
            {
                return 200;
            }
            else
            {
                _appDbContext!.Remove(productToRemove);
                var result = await _appDbContext.SaveChangesAsync();

                await Task.CompletedTask;
                return result;
            }
        }
        catch (Exception e)
        {
            return -1;
        }
    }

    public async Task<int> AddAsync(Product product)
    {
        try
        {
            _appDbContext!.Products.Add(product);
            var result = _appDbContext.SaveChanges();

            await Task.CompletedTask;
            return result;
        }
        catch (Exception e)
        {
            return -1;
        }
    }

    public async Task<Product?> GetAsync(int id)
    {
        try
        {
            var product = await Task.FromResult(
                _appDbContext!.Products.FirstOrDefault(product => product.Id == id)
            );

            return await Task.FromResult(product);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<IEnumerable<Product>> BrowseAllAsync()
    {
        try
        {
            IEnumerable<Product> products = await Task.FromResult(_appDbContext!.Products);

            return products;
        }
        catch (Exception e)
        {
            return new List<Product>();
        }
    }
}