using Common;
using LanguageExt.ClassInstances;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "CreateProduct")]
    public async Task<IActionResult> Create([FromBody] Product product, CancellationToken ct)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5001");
        var response = await client.PostAsJsonAsync("/product", product, ct);
        if (response.IsSuccessStatusCode is false)
        {
            return BadRequest("新增失敗");
        }

        return Ok("新增成功");
    }
}
