using Common;
using LanguageExt.Common;

namespace WebApplication1.Repository;

public class UserRepository
{
    //Get
    public async Task<Result<User>> GetAsync(int userId, CancellationToken ct)
    {
        if (userId == 0)
        {
            return new Result<User>(new InvalidDataException("UserId 不符合規範"));
        }

        var result = await ApiHelper.GetAsync<User>($"/user/{userId}", ct);
        return result;
    }
}
