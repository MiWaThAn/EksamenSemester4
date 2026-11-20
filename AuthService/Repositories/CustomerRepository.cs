using AuthService.Data;
using AuthService.Entities;
using Microsoft.EntityFrameworkCore;
namespace AuthService.Repositories
{
    public class CustomerRepository : UserRepository, ICustomerRepository
    {
        public CustomerRepository(AuthContext context) : base(context) { }



        public new async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<Customer?> GetByUsernameAsync(string username)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Username == username);
        }
    }
}
