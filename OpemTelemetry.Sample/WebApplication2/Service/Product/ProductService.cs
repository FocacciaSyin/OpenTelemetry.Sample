using Common.AOP;
using WebApplication2.Repository.Product;

namespace WebApplication2.Service.Product;

[Tracing]
public class ProductService(ProductRepository productRepository)
{
    //Create
    public async Task<int> CreateAsync(Common.Models.Product product, CancellationToken ct)
    {
        var result = await productRepository.CreateAsync(product, ct);
        return result;
    }

    //GetAll
    public async Task<IEnumerable<Common.Models.Product>> GetAllAsync(CancellationToken ct)
    {
        var result = await productRepository.GetAllAsync(ct);
        return result;
    }

    //GetById
    public async Task<Common.Models.Product> GetByIdAsync(int id, CancellationToken ct)
    {
        var result = await productRepository.GetByIdAsync(id, ct);
        return result;
    }
}
