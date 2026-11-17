using AuthService.Entities;
using AuthService.Entities.Enums;

namespace AuthService.Repositories
{
    public interface IEmployeeRepository : IUserRepository
    {


        Task<IEnumerable<Employee>> GetByDepartmentAsync(Department department);


    }
}
