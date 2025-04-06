using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Repository;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Common.Models.User> Users => Set<Common.Models.User>();
    public DbSet<Common.Models.Product> Products => Set<Common.Models.Product>();
}
