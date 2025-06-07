using Common;
using Common.AOP;
using Common.Models;
using LanguageExt.ClassInstances;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[Tracing]
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger, 
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost(Name = "CreateProduct")]
    public async Task<IActionResult> Create([FromBody] Product product, CancellationToken ct)
    {
        return BadRequest("拋出BadRequest");
        var httpClient = _httpClientFactory.CreateClient("Default");
        var response = await httpClient.PostAsJsonAsync("/product", product, ct);
        if (response.IsSuccessStatusCode is false)
        {
            return BadRequest("新增失敗");
        }

        return Ok("新增成功");
    }

    [HttpGet(Name = "GetProducts")]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        _logger.LogTrace("[Woody] Debug");
        var httpClient = _httpClientFactory.CreateClient("Default");
        var response = await httpClient.GetAsync("/product", ct);
        if (response.IsSuccessStatusCode is false)
        {
            return BadRequest("獲取產品列表失敗");
        }

        var products = await response.Content.ReadFromJsonAsync<List<Product>>(cancellationToken: ct);
        return Ok(products);
    }

    [HttpGet("{id}", Name = "GetProductById")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var httpClient = _httpClientFactory.CreateClient("Default");
        var response = await httpClient.GetAsync($"/product/{id}", ct);
        if (response.IsSuccessStatusCode is false)
        {
            return BadRequest("獲取產品失敗");
        }

        var product = await response.Content.ReadFromJsonAsync<Product>(cancellationToken: ct);
        return Ok(product);
    }
}
