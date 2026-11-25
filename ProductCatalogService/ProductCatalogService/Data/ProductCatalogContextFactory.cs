using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProductCatalogService.Data
{
    public class ProductCatalogContextFactory : IDesignTimeDbContextFactory<ProductCatalogContext>
    {
        public ProductCatalogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductCatalogContext>();
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=productdb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new ProductCatalogContext(optionsBuilder.Options);
        }
    }
}
