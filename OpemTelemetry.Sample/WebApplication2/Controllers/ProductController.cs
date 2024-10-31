using Common;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Service.Product;

namespace WebApplication2.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(ProductService productService) : ControllerBase
{
    [HttpPost(Name = "CreateProduct")]
    public async Task<int> Create([FromBody] Product product, CancellationToken ct)
    {
        var result = await productService.CreateAsync(product, ct);
        return result;
    }
}
