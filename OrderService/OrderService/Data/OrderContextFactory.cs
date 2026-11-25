using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace OrderService.Data
{

        public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
        {
            public OrderContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
                optionsBuilder.UseSqlServer(
                    "Server=(localdb)\\mssqllocaldb;Database=orderdb;Trusted_Connection=True;TrustServerCertificate=True;");

                return new OrderContext(optionsBuilder.Options);
            }
        }
}


