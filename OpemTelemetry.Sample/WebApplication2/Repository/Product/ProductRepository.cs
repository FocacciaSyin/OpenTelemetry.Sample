using Common.AOP;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Repository.Product;

[Tracing]
public class ProductRepository
{
    private readonly DataContext _dataContext;

    public ProductRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    //GetById
    public async Task<IEnumerable<Common.Product>> GetByUserId(int userId, CancellationToken ct)
    {
        var result = await _dataContext.Products
            .Where(x => x.UserId == userId)
            .AsNoTracking()
            .ToListAsync(ct);

        return result;
    }

    //Add
    public async Task<int> CreateAsync(Common.Product product, CancellationToken ct)
    {
        _dataContext.Products.Add(product);
        var result = await _dataContext.SaveChangesAsync();
        return result;
    }

    //Delete
    public void Delete(int id)
    {
        var product = _dataContext.Products.Find(id);
        _dataContext.Products.Remove(product);
        _dataContext.SaveChanges();
    }
}
