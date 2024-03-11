using Bogus;
using OpenTelemetry.WebService.Product.Repository.Interfaces;
using OpenTelemetry.WebService.Product.Repository.Models;

namespace OpenTelemetry.WebService.Product.Repository.Implements;

public class ProductRepository : IProductRepository
{
    /// <summary>
    /// 取得產品列表
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<ProductDataModel>> GetAsync()
    {
        var fakerProduct = new Faker<ProductDataModel>();

        int orderIds = 1;

        var results = fakerProduct
            .StrictMode(true)
            .RuleFor(o => o.Id, f => orderIds++)
            .RuleFor(o => o.ProductNo, f => f.Finance.Account())
            .RuleFor(o => o.Name, f => f.Lorem.Letter(50))
            .RuleFor(o => o.Price, f => f.Random.Double(10, 1000))
            .RuleFor(o => o.CreateDate, f => f.Date.Future())
            .RuleFor(o => o.UpdateDate, f => null)
            .Generate(1000);

        return results;
    }
}
