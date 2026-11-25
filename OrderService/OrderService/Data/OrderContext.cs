using Microsoft.EntityFrameworkCore;
using OrderService.Entities;

namespace OrderService.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderLine> OrderLines => Set<OrderLine>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");
                entity.HasKey(o => o.Id);
                entity.Property(o => o.TotalPrice).HasPrecision(18, 2);
                entity.HasIndex(o => o.CustomerId);
                entity.ComplexProperty(o => o.DeliveryAddress);
                entity.ComplexProperty(o => o.BillingAddress);
                entity.HasMany(o => o.Lines)
                    .WithOne()
                    .HasForeignKey(l => l.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Navigation(o => o.Lines)
                    .UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            modelBuilder.Entity<OrderLine>(entity =>
            {
                entity.ToTable("OrderLines");
                entity.HasKey(l => l.Id);
                entity.Property(l => l.UnitPrice).HasPrecision(18, 2);
                entity.HasIndex(l => l.ProductId);
                entity.Ignore(l => l.LineTotal);
            });
        }



    }
    
}
