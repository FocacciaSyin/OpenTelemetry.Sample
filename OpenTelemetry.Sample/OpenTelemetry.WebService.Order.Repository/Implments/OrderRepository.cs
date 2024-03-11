using Bogus;
using OpenTelemetry.WebService.Order.Repository.Intrerfaces;
using OpenTelemetry.WebService.Order.Repository.Models;

namespace OpenTelemetry.WebService.Order.Repository.Implments;

public class OrderRepository : IOrderRepository
{
    /// <summary>
    /// 取得訂單
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<OrderDataModel>> GetAsync()
    {
        var fakerOrder = new Faker<OrderDataModel>();

        int orderIds = 1;

        var results = fakerOrder
            .StrictMode(true)
            .RuleFor(o => o.Id, f => orderIds++)
            .RuleFor(o => o.OrderNo, f => f.Finance.Account())
            .RuleFor(o => o.Description, f => f.Lorem.Letter(50))
            .RuleFor(o => o.CreateDate, f => f.Date.Future())
            .RuleFor(o => o.UpdateDate, f => null)
            .Generate(1000);

        return results;
    }
}
