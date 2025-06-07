using Common;
using Common.AOP;
using Common.Models;
using WebApplication2.Repository.Product;
using WebApplication2.Repository.User;

namespace WebApplication2.Service.User;

[Tracing]
public class UserService(UserRepository userRepository, ProductRepository productRepository)
{
    //Get
    public async Task<Common.Models.User?> GetAsync(int userId, CancellationToken ct)
    {
        return await userRepository.GetAsync(userId, ct);
    }
    
    //Create
    public async Task<int> CreateAsync(Common.Models.User user, CancellationToken ct)
    {
        var result = await userRepository.CreateAsync(user, ct);
        return result;
    }

    public async Task<UserProductDataModel> GetUserProductAsync(int userId, CancellationToken ct)
    {
        var user = await userRepository.GetAsync(userId, ct);

        if (user is null)
            return new UserProductDataModel();

        var userProducts = await productRepository.GetByUserId(userId, ct);
        var userProductDataModel = new UserProductDataModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            ProductDataModels = userProducts
        };

        return userProductDataModel;
    }
}
