using OpenTelemetry.WebService.Product.Service.Dtos;

namespace OpenTelemetry.WebService.Product.Service.Interfaces;

public interface IProductService
{
    /// <summary>
    /// 取得產品列表
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ProductDto>> GetAsync();
}