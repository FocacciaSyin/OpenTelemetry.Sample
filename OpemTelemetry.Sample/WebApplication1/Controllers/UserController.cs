using Common;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Infrastructure.Helper;
using WebApplication1.Repository;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserRepository _userRepository;
    private readonly ApiHelper _apiHelper;

    public UserController(ILogger<UserController> logger, UserRepository userRepository, ApiHelper apiHelper)
    {
        _logger = logger;
        _userRepository = userRepository;
        _apiHelper = apiHelper;
    }

    [HttpGet("{userId}", Name = "GetUserById")]
    public async Task<IActionResult> Get(int userId, CancellationToken ct)
    {
        //這種Result寫法是 參考：https://www.youtube.com/watch?v=aksjZkCbIWA
        var result = await _userRepository.GetAsync(userId, ct);

        return result.Match<IActionResult>(
            s => Ok(s),
            exception => BadRequest(exception));
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<IActionResult> Create([FromBody] User user, CancellationToken ct)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5001");
        var response = await client.PostAsJsonAsync("/user", user, ct);
        if (response.IsSuccessStatusCode is false)
        {
            return BadRequest("新增失敗");
        }

        return Ok("新增成功");
    }

    [HttpGet("Product/{userId}", Name = "GetUserProductByUserId")]
    public async Task<IActionResult> GetUserProduct(int userId, CancellationToken ct)
    {
        var result = await _apiHelper.GetAsync<UserProductDataModel>($"user/product/{userId}", ct);

        return result.Match<IActionResult>(
            s => Ok(s),
            exception => BadRequest(exception));
    }
}
