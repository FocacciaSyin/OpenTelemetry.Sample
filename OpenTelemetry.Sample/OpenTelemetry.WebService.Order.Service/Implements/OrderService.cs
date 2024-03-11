using System.Security.Cryptography;
using Bogus;
using MapsterMapper;
using OpenTelemetry.WebService.Order.Repository.Intrerfaces;
using OpenTelemetry.WebService.Order.Service.Dtos;
using OpenTelemetry.WebService.Order.Service.Interfaces;

namespace OpenTelemetry.WebService.Order.Service.Implements;

public class OrderService(
    IMapper mapper,
    IOrderRepository orderRepository) :
    IOrderService
{
    /// <summary>
    /// 取得訂單
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<OrderDto>> GetAsync()
    {
        var results = await orderRepository.GetAsync();
        return mapper.Map<IEnumerable<OrderDto>>(results);
    }
}
