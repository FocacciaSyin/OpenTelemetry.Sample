using OpenTelemetry.WebService.Order.Service.Dtos;

namespace OpenTelemetry.WebService.Order.Service.Interfaces;

public interface IOrderService
{
    /// <summary>
    /// 取得訂單
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<OrderDto>> GetAsync();
}