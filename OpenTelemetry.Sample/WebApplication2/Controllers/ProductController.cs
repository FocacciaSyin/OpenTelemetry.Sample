using Common;
using Common.Models;
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

    [HttpGet(Name = "GetProducts")]
    public async Task<IEnumerable<Product>> GetAll(CancellationToken ct)
    {
        var result = await productService.GetAllAsync(ct);
        return result;
    }

    [HttpGet("{id}", Name = "GetProductById")]
    public async Task<Product> GetById(int id, CancellationToken ct)
    {
        var result = await productService.GetByIdAsync(id, ct);
        return result;
    }
}
