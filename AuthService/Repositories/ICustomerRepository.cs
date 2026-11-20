using AuthService.Entities;

namespace AuthService.Repositories
{
    public interface ICustomerRepository : IUserRepository
    {
        Task<Customer?> GetByUsernameAsync(string username);
        Task<Customer?> GetByIdAsync(Guid id);
    }
}
