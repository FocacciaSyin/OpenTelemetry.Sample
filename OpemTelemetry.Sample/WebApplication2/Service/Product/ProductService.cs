using Common.AOP;
using WebApplication2.Repository.Product;

namespace WebApplication2.Service.Product;

[Tracing]
public class ProductService(ProductRepository productRepository)
{
    //Create
    public async Task<int> CreateAsync(Common.Product product, CancellationToken ct)
    {
        var result = await productRepository.CreateAsync(product, ct);
        return result;
    }
}
