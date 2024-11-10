using Common;
using Common.AOP;
using LanguageExt.Common;
using WebApplication1.Infrastructure.Helper;

namespace WebApplication1.Repository;

[Tracing]
public class UserRepository
{
    private readonly ApiHelper _apiHelper;

    public UserRepository(ApiHelper apiHelper)
    {
        _apiHelper = apiHelper;
    }

    //Get
    public async Task<Result<User>> GetAsync(int userId, CancellationToken ct)
    {
        if (userId == 0)
        {
            return new Result<User>(new InvalidDataException("UserId 不符合規範"));
        }

        var result = await _apiHelper.GetAsync<User>($"/user/{userId}", ct);
        return result;
    }
}
