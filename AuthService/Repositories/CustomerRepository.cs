using AuthService.Data;
using AuthService.Entities;
using Microsoft.EntityFrameworkCore;
namespace AuthService.Repositories
{
    public class CustomerRepository : UserRepository, ICustomerRepository
    {
        public CustomerRepository(AuthContext context) : base(context) { }

        public async Task<Customer?> GetByUsernameAsync(string username)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Username == username);
        }
    }
}
