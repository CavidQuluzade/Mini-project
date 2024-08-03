using Microsoft.EntityFrameworkCore;
using Core.Constats.SqlConstats;
using Core.Entities;

namespace Data;

public class ConsoleCommerceAppDbContext : DbContext
{
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(SqlConnectionStrings.MsSqlConnectionString);
    }

}
