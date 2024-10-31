using Common;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Service.User;

namespace WebApplication2.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(UserService userService) : ControllerBase
{
    [HttpGet("{userId}", Name = "GetUserById")]
    public async Task<User> Get(int userId, CancellationToken ct)
    {
        var result = await userService.GetAsync(userId, ct);
        return result;
    }

    //Create User
    [HttpPost(Name = "CreateUser")]
    public async Task<int> Create([FromBody] User user, CancellationToken ct)
    {
        var result = await userService.CreateAsync(user, ct);
        return result;
    }


    [HttpGet("Product/{userId}", Name = "GetUserProductByUserId")]
    public async Task<UserProductDataModel> GetUserProduct(int userId, CancellationToken ct)
    {
        var result = await userService.GetUserProductAsync(userId, ct);
        return result;
    }
}
