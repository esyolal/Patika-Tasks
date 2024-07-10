using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
namespace Task1_RestfulApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private List<Product> products;

        public ProductController()
        {
            products = new List<Product>();
            products.Add(new Product() { Id = 1, Name = "Ayakkabı", Price = 10.0m, Color = "Red" });
            products.Add(new Product() { Id = 2, Name = "Tişört", Price = 15.0m, Color = "Blue" });
            products.Add(new Product() { Id = 3, Name = "Kravat", Price = 5.0m, Color = "Black" });
            products.Add(new Product() { Id = 4, Name = "Kravat", Price = 25.0m, Color = "Yellow" });
            products.Add(new Product() { Id = 5, Name = "Kravat", Price = 85.0m, Color = "White" });
        }
        //1.Derste yaptığımız apiresponse sınıfını örnek olarak kullandım.
        [HttpGet]
        public ApiResponse<List<Product>> Get()
        {
            return new ApiResponse<List<Product>>(products);
        }
        [HttpGet("{id}")]
        public ApiResponse<Product> Get(int id)
        {
            var item = products?.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return new ApiResponse<Product>("Item not found in system.");
            }

            return new ApiResponse<Product>(item);
        }
        [HttpGet("byname")]
        public ActionResult<IEnumerable<Product>> GetProductsByName([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name parameter is required.");
            }

            var productsByName = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (productsByName.Count == 0)
            {
                return NotFound("No products found with the specified name.");
            }

            return Ok(new ApiResponse<List<Product>>(productsByName));
        }
        [HttpGet("bycolor")]
        public ActionResult<IEnumerable<Product>> GetProductsByColor([FromQuery] string color)
        {
            if (string.IsNullOrEmpty(color))
            {
                return BadRequest("Name parameter is required.");
            }

            var productsByColor = products.Where(p => p.Color.Contains(color, StringComparison.OrdinalIgnoreCase)).ToList();

            if (productsByColor.Count == 0)
            {
                return NotFound("No products found with the specified color.");
            }

            return Ok(new ApiResponse<List<Product>>(productsByColor));
        }
        [HttpGet("byprice")]
        public ActionResult<IEnumerable<Product>> GetProductsByColor([FromQuery] decimal price)
        {
            if (price == 0)
            {
                return BadRequest("Price parameter is required.");
            }
            if (price < 0)
            {
                return BadRequest("Price parameter must be a non-negative number.");
            }

            var productsByPrice = products.Where(p => p.Price >= price).OrderBy(p => p.Price).ToList();

            if (productsByPrice.Count == 0)
            {
                return NotFound("No products found with the specified color.");
            }

            return Ok(new ApiResponse<List<Product>>(productsByPrice));
        }
        /* [HttpPost]
        public ApiResponse<List<Product>> Post([FromBody] Product value)
        {
            products.Add(value);
            return new ApiResponse<List<Product>>(products);
        } */
        [HttpPost]
        public ApiResponse<List<Product>> Post([FromQuery] string name, [FromQuery] decimal price, [FromQuery] string color)
        {
            var product = new Product
            {
                Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1,
                Name = name,
                Price = price,
                Color = color
            };

            products.Add(product);
            return new ApiResponse<List<Product>>(products);
        }

        [HttpPut("{id}")]
        public ApiResponse<List<Product>> Put(int id, [FromBody] Product value)
        {
            var item = products.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return new ApiResponse<List<Product>>("Item not found in system.");
            }

            products.Remove(item);
            products.Add(value);
            return new ApiResponse<List<Product>>(products);
        }

        [HttpDelete("{id}")]
        public ApiResponse<List<Product>> Delete(int id)
        {
            var item = products.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return new ApiResponse<List<Product>>("Item not found in system.");
            }

            products.Remove(item);
            return new ApiResponse<List<Product>>(products);
        }
        [HttpPatch("{id}")]
        public ApiResponse<Product> PatchProduct(int id, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            if (patchDoc == null)
            {
                return new ApiResponse<Product>("Patch document cannot be null.");
            }

            var existingProduct = products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return new ApiResponse<Product>($"Product with id {id} not found.");
            }

            patchDoc.ApplyTo(existingProduct, ModelState);

            if (!ModelState.IsValid)
            {
                return new ApiResponse<Product>("Invalid model state.");
            }

            return new ApiResponse<Product>(existingProduct);
        }
        [HttpGet("products")]
        public IActionResult GetFilteredProducts([FromQuery] string name, [FromQuery] string sort, [FromQuery] string color)
        {
            var filteredProducts = products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                filteredProducts = filteredProducts.Where(p => p.Name.Contains(name, System.StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(color))
            {
                filteredProducts = filteredProducts.Where(p => p.Color.Contains(color, System.StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "name":
                        filteredProducts = filteredProducts.OrderBy(p => p.Name);
                        break;
                    case "price":
                        filteredProducts = filteredProducts.OrderBy(p => p.Price);
                        break;
                    case "color":
                        filteredProducts = filteredProducts.OrderBy(p => p.Color);
                        break;
                }
            }
            return Ok(filteredProducts.ToList());
        }
        [HttpGet("ByIdQuery")]
        public Product ByIdQuery([FromQuery] int id)
        {
            return products?.FirstOrDefault(x => x.Id == id);
        }
        [HttpGet("ByIdRoute/{id}")]
        public Product ByIdRoute([FromRoute] int id)
        {
            return products?.FirstOrDefault(x => x.Id == id);
        }

    }
}
