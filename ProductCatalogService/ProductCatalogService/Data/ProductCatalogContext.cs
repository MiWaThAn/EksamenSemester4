using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Entities;

namespace ProductCatalogService.Data
{
    public class ProductCatalogContext : DbContext
    {
        public ProductCatalogContext(DbContextOptions<ProductCatalogContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.HasIndex(p => p.Barcode)
              .IsUnique();


            });
        }
    }
}