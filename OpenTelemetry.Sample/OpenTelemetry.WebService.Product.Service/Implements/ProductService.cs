using MapsterMapper;
using OpenTelemetry.WebService.Product.Repository.Interfaces;
using OpenTelemetry.WebService.Product.Service.Dtos;
using OpenTelemetry.WebService.Product.Service.Interfaces;

namespace OpenTelemetry.WebService.Product.Service.Implements;

public class ProductService(
    IMapper mapper,
    IProductRepository productRepository) :
    IProductService
{
    /// <summary>
    /// 取得產品列表
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<ProductDto>> GetAsync()
    {
        var results = await productRepository.GetAsync();
        return mapper.Map<IEnumerable<ProductDto>>(results);
    }
}
