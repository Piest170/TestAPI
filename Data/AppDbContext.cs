using Microsoft.EntityFrameworkCore;
using TestAPI.Models;

namespace TestAPI.Data;

// Data Center
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products => Set<Product>();
}