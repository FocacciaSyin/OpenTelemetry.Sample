using OpenTelemetry.WebService.Order.Repository.Models;

namespace OpenTelemetry.WebService.Order.Repository.Intrerfaces;

public interface IOrderRepository
{
    /// <summary>
    /// 取得訂單
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<OrderDataModel>> GetAsync();
}