using OpenTelemetry.WebService.Product.Repository.Models;

namespace OpenTelemetry.WebService.Product.Repository.Interfaces;

public interface IProductRepository
{
    /// <summary>
    /// 取得產品列表
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ProductDataModel>> GetAsync();
}