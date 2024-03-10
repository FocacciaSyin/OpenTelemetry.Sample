using Bogus;
using OpenTelemetry.WebService.Product.Service.Dtos;
using OpenTelemetry.WebService.Product.Service.Interfaces;

namespace OpenTelemetry.WebService.Product.Service.Implements;

public class ProductService : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetAsync()
    {
        var fakerOrder = new Faker<ProductDto>();

        int orderIds = 1;

        var results = fakerOrder
            .StrictMode(true)
            .RuleFor(o => o.Id, f => orderIds++)
            .RuleFor(o => o.ProductNo, f => f.Finance.Account(8))
            .RuleFor(o => o.Name, f => f.Lorem.Letter(50))
            .RuleFor(o => o.Price, f => f.Random.Double(10, 1000))
            .RuleFor(o => o.CreateDate, f => f.Date.Future())
            .RuleFor(o => o.UpdateDate, f => null)
            .Generate(1000);

        return results;
    }
}
