using Microsoft.EntityFrameworkCore;
using WebApi.Models.Entities;

namespace WebApi.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext()
        {

        }

        public SqlContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }
    }
}
