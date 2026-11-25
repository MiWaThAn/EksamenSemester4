using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AuthService.Data
{
    public class AuthContextFactory : IDesignTimeDbContextFactory<AuthContext>
    {
        public AuthContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthContext>();
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=authdb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new AuthContext(optionsBuilder.Options);
        }
    }
}
