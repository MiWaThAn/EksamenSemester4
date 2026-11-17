using AuthService.Data;
using AuthService.Entities;
using AuthService.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repositories
{
    public class EmployeeRepository : UserRepository, IEmployeeRepository
    {
        public EmployeeRepository(AuthContext context) : base(context) { }

        public async Task<IEnumerable<Employee>> GetByDepartmentAsync(Department department)
        {
            return await _context.Employees.Where(e => e.Department == department).ToListAsync();
        }
    }
}
