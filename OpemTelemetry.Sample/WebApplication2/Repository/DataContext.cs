using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Repository;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Common.User> Users => Set<Common.User>();
    public DbSet<Common.Product> Products => Set<Common.Product>();
}
