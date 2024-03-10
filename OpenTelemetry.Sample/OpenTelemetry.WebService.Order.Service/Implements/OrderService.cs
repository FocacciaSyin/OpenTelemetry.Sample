using Bogus;
using OpenTelemetry.WebService.Order.Service.Dtos;
using OpenTelemetry.WebService.Order.Service.Interfaces;

namespace OpenTelemetry.WebService.Order.Service.Implements;

public class OrderService : IOrderService
{
    /// <summary>
    /// 取得訂單
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<OrderDto>> GetAsync()
    {
        var fakerOrder = new Faker<OrderDto>();

        int orderIds = 1;

        var results = fakerOrder
            .StrictMode(true)
            .RuleFor(o => o.Id, f => orderIds++)
            .RuleFor(o => o.OrderNo, f => f.Finance.Account(8))
            .RuleFor(o => o.Description, f => f.Lorem.Letter(50))
            .RuleFor(o => o.CreateDate, f => f.Date.Future())
            .RuleFor(o => o.UpdateDate, f => null)
            .Generate(1000);

        return results;
    }
}
