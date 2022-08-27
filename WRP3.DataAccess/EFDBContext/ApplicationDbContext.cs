using Microsoft.EntityFrameworkCore;
using WRP3.Domain.Entities;

namespace WRP3.DataAccess.EFDBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTest> ProductTests { get; set; }
        public DbSet<TestType> TestTypes { get; set; }

    }
}
