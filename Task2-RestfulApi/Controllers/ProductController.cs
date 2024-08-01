using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task2_restfulapi.Services;

namespace task2_restfulapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public ProductController(IProductService productService, IUserService userService)
        {
            _userService = userService;
            _productService = productService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (_userService.ValidateUser(request.Username, request.Password))
            {
                return Ok(new { Message = "Login successful" });
            }

            return Unauthorized(new { Message = "Invalid username or password" });
        }
        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<Product>>> Get()
        {
            var products = await _productService.GetProductsAsync();
            return new ApiResponse<IEnumerable<Product>>(products);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<Product>> Get(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return new ApiResponse<Product>("Product not found.");
            }
            return new ApiResponse<Product>(product);
        }

        [HttpGet("byname")]
        public async Task<ApiResponse<IEnumerable<Product>>> GetProductsByName([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new ApiResponse<IEnumerable<Product>>("Name parameter is required.");
            }

            var products = await _productService.GetProductsAsync();
            var productsByName = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!productsByName.Any())
            {
                return new ApiResponse<IEnumerable<Product>>("No products found with the specified name.");
            }

            return new ApiResponse<IEnumerable<Product>>(productsByName);
        }

        [HttpGet("bycolor")]
        public async Task<ApiResponse<IEnumerable<Product>>> GetProductsByColor([FromQuery] string color)
        {
            if (string.IsNullOrEmpty(color))
            {
                return new ApiResponse<IEnumerable<Product>>("Color parameter is required.");
            }

            var products = await _productService.GetProductsAsync();
            var productsByColor = products.Where(p => p.Color.Contains(color, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!productsByColor.Any())
            {
                return new ApiResponse<IEnumerable<Product>>("No products found with the specified color.");
            }

            return new ApiResponse<IEnumerable<Product>>(productsByColor);
        }

        [HttpGet("byprice")]
        public async Task<ApiResponse<IEnumerable<Product>>> GetProductsByPrice([FromQuery] decimal price)
        {
            if (price < 0)
            {
                return new ApiResponse<IEnumerable<Product>>("Price parameter must be a non-negative number.");
            }

            var products = await _productService.GetProductsAsync();
            var productsByPrice = products.Where(p => p.Price >= price).OrderBy(p => p.Price).ToList();

            if (!productsByPrice.Any())
            {
                return new ApiResponse<IEnumerable<Product>>("No products found with the specified price.");
            }

            return new ApiResponse<IEnumerable<Product>>(productsByPrice);
        }

        [HttpPost]
        public async Task<ApiResponse<IEnumerable<Product>>> Post([FromBody] Product product)
        {
            if (product == null)
            {
                return new ApiResponse<IEnumerable<Product>>("Product cannot be null.");
            }

            await _productService.AddProductAsync(product);
            var products = await _productService.GetProductsAsync();
            return new ApiResponse<IEnumerable<Product>>(products);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<IEnumerable<Product>>> Put(int id, [FromBody] Product updatedProduct)
        {
            if (updatedProduct == null)
            {
                return new ApiResponse<IEnumerable<Product>>("Product cannot be null.");
            }

            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return new ApiResponse<IEnumerable<Product>>("Product not found.");
            }

            updatedProduct.Id = id; // Ensure the ID is preserved
            await _productService.UpdateProductAsync(updatedProduct);
            var products = await _productService.GetProductsAsync();
            return new ApiResponse<IEnumerable<Product>>(products);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse<IEnumerable<Product>>> Delete(int id)
        {
            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return new ApiResponse<IEnumerable<Product>>("Product not found.");
            }

            await _productService.DeleteProductAsync(id);
            var products = await _productService.GetProductsAsync();
            return new ApiResponse<IEnumerable<Product>>(products);
        }

        [HttpPatch("{id}")]
        public async Task<ApiResponse<Product>> PatchProduct(int id, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            if (patchDoc == null)
            {
                return new ApiResponse<Product>("Patch document cannot be null.");
            }

            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return new ApiResponse<Product>($"Product with id {id} not found.");
            }

            patchDoc.ApplyTo(existingProduct, ModelState);

            if (!ModelState.IsValid)
            {
                return new ApiResponse<Product>("Invalid model state.");
            }

            await _productService.UpdateProductAsync(existingProduct);
            return new ApiResponse<Product>(existingProduct);
        }

        [HttpGet("products")]
        public async Task<ApiResponse<IEnumerable<Product>>> GetFilteredProducts([FromQuery] string name, [FromQuery] string sort, [FromQuery] string color)
        {
            var products = await _productService.GetProductsAsync();
            var filteredProducts = products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                filteredProducts = filteredProducts.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(color))
            {
                filteredProducts = filteredProducts.Where(p => p.Color.Contains(color, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(sort))
            {
                filteredProducts = sort.ToLower() switch
                {
                    "name" => filteredProducts.OrderBy(p => p.Name),
                    "price" => filteredProducts.OrderBy(p => p.Price),
                    "color" => filteredProducts.OrderBy(p => p.Color),
                    _ => filteredProducts
                };
            }

            return new ApiResponse<IEnumerable<Product>>(filteredProducts.ToList());
        }

        [HttpGet("ByIdQuery")]
        public async Task<ApiResponse<Product>> ByIdQuery([FromQuery] int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return new ApiResponse<Product>("Product not found.");
            }
            return new ApiResponse<Product>(product);
        }

        [HttpGet("ByIdRoute/{id}")]
        public async Task<ApiResponse<Product>> ByIdRoute([FromRoute] int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return new ApiResponse<Product>("Product not found.");
            }
            return new ApiResponse<Product>(product);
        }

        [HttpGet("ByNameRoute/{name}")]
        public async Task<ApiResponse<Product>> ByNameRoute([FromRoute] string name)
        {
            var products = await _productService.GetProductsAsync();
            var product = products.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (product == null)
            {
                return new ApiResponse<Product>("Product not found.");
            }
            return new ApiResponse<Product>(product);
        }
    }
}
