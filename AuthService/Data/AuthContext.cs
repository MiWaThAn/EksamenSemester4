using AuthService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace AuthService.Data
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);

                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Email).IsRequired().HasMaxLength(255);

                entity.OwnsOne(u => u.Name, name =>
                {
                    name.Property(n => n.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(100);
                    name.Property(n => n.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(100);
                });
                entity.Property(e => e.Role).HasConversion<string>().HasMaxLength(50).IsRequired();
                entity.Property(u => u.PasswordHashed).IsRequired();
                entity.Property(u => u.CreatedAt).IsRequired();
            });

            
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");

                entity.Property(c => c.Username).IsRequired().HasMaxLength(50);
                entity.Property(c => c.PhoneNumber).HasMaxLength(30);

                
                entity.OwnsOne(c => c.DefaultAddress, address =>
                {
                    address.Property(a => a.StreetNumber).HasColumnName("Address_StreetNumber").HasMaxLength(20);
                    address.Property(a => a.Street).HasColumnName("Address_Street").HasMaxLength(150);
                    address.Property(a => a.City).HasColumnName("Address_City").HasMaxLength(100);
                    address.Property(a => a.ZipCode).HasColumnName("Address_ZipCode").HasMaxLength(20);
                    address.Property(a => a.Country).HasColumnName("Address_Country").HasMaxLength(100);
                });

               
                entity.OwnsOne(c => c.PaymentInformation, payment =>
                {
                    payment.Property(p => p.GatewayCustomerReferenceId).HasColumnName("Payment_GatewayRefId").HasMaxLength(100);
                    payment.Property(p => p.CardProvider).HasColumnName("Payment_CardProvider").HasMaxLength(50);
                    payment.Property(p => p.LastFourDigits).HasColumnName("Payment_LastFourDigits").HasMaxLength(4);
                });
            });

           
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employees");

                entity.Property(e => e.Department).HasConversion<string>().HasMaxLength(50).IsRequired();



                var permissionComparer = new ValueComparer<List<string>>(
                        (a, b) => (a ?? new List<string>()).SequenceEqual(b ?? new List<string>()),
                         c => (c ?? new List<string>()).Aggregate(0, (acc, item) => HashCode.Combine(acc, item.GetHashCode())),
                        c => (c ?? new List<string>()).ToList());

                entity.Property<List<string>>("_permissions")
                    .HasColumnName("Permissions")
                    .HasConversion(
                        v => JsonSerializer.Serialize(v ?? new List<string>(), (JsonSerializerOptions?)null),
                        v => string.IsNullOrWhiteSpace(v)
                            ? new List<string>()
                            : JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>())
                    .Metadata.SetValueComparer(permissionComparer);
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshTokens");
                entity.HasKey(rt => rt.Id);

                entity.Property(rt => rt.TokenHash)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasIndex(rt => rt.TokenHash)
                    .IsUnique();

                entity.HasIndex(rt => rt.UserId);

                entity.Property(rt => rt.CreatedAt).IsRequired();
                entity.Property(rt => rt.ExpiresAt).IsRequired();

                entity.HasOne(rt => rt.User)
                    .WithMany()
                    .HasForeignKey(rt => rt.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });



        }
    }
}