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
                entity.ToTable("Products");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Price).HasPrecision(18, 2);
                entity.HasIndex(p => p.Barcode)
              .IsUnique();


            });
        }
    }
}