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
    public async Task<IEnumerable<Common.Models.Product>> GetByUserId(int userId, CancellationToken ct)
    {
        var result = await _dataContext.Products
            .Where(x => x.UserId == userId)
            .AsNoTracking()
            .ToListAsync(ct);

        return result;
    }

    //Add
    public async Task<int> CreateAsync(Common.Models.Product product, CancellationToken ct)
    {
        _dataContext.Products.Add(product);
        var result = await _dataContext.SaveChangesAsync();
        return result;
    }

    public void Delete(int id)
    {
        var product = _dataContext.Products.Find(id);
        _dataContext.Products.Remove(product);
        _dataContext.SaveChanges();
    }

    //GetAll
    public async Task<IEnumerable<Common.Models.Product>> GetAllAsync(CancellationToken ct)
    {
        var result = await _dataContext.Products
            .AsNoTracking()
            .ToListAsync(ct);
        return result;
    }

    //GetById
    public async Task<Common.Models.Product> GetByIdAsync(int id, CancellationToken ct)
    {
        var result = await _dataContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        return result;
    }
}
