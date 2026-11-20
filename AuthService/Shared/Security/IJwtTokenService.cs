using AuthService.Features.Shared.Customer;
using AuthService.Features.Shared.Employee;

namespace AuthService.Shared.Security
{
    public interface IJwtTokenService
    {
        string GenerateCustomerToken(CustomerAuthDTO customer);
        string GenerateEmployeeToken(EmployeeAuthDTO employee);
  
    }
}
