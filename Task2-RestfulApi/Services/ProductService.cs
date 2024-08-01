using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace task2_restfulapi.Services;
public class ProductService : IProductService
{
    private readonly List<Product> _products;

    public ProductService()
    {
        _products = new List<Product>
        {
            new Product { Id = 1, Name = "Ayakkabı", Price = 10.0m, Color = "Red" },
            new Product { Id = 2, Name = "Tişört", Price = 15.0m, Color = "Blue" },
            new Product { Id = 3, Name = "Kravat", Price = 5.0m, Color = "Black" },
            new Product { Id = 4, Name = "Kravat", Price = 25.0m, Color = "Yellow" },
            new Product { Id = 5, Name = "Kravat", Price = 85.0m, Color = "White" }
        };
    }

    public Task<IEnumerable<Product>> GetProductsAsync()
    {
        return Task.FromResult(_products.AsEnumerable());
    }

    public Task<Product> GetProductByIdAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }

    public Task AddProductAsync(Product product)
    {
        product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
        _products.Add(product);
        return Task.CompletedTask;
    }

    public Task UpdateProductAsync(Product product)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            _products.Remove(existingProduct);
            _products.Add(product);
        }
        return Task.CompletedTask;
    }

    public Task DeleteProductAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            _products.Remove(product);
        }
        return Task.CompletedTask;
    }
}
