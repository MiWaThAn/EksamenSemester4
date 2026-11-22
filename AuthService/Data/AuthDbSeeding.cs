using AuthService.Entities;
using AuthService.Entities.Enums;
using AuthService.Repositories;
using AuthService.Shared.Security;


namespace AuthService.Data
{
    public class AuthDbSeeding
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        public AuthDbSeeding(IPasswordHasher passwordHasher, IEmployeeRepository employeeRepository, ICustomerRepository customerRepository, IUserRepository userRepository )
        {
            _passwordHasher = passwordHasher;
            _employeeRepository = employeeRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public async Task SeedAsync(AuthContext dbContext, CancellationToken cancellationToken = default)
        {
            

            
             string customerEmail = "customer@test.local";
            var userCustomer = await _userRepository.GetByEmailAsync(customerEmail, cancellationToken);
            if (userCustomer == null)
            {
                var customer = Customer.Create(
                    firstName: "Test",
                    lastName: "Kunde",
                    email: customerEmail,
                    passwordHashed: _passwordHasher.HashPassword("ABCD1234"),
                    mfaEnabled: false,
                    username: "testkunde",
                    phoneNumber: "+45 12345678");

                customer.UpdateDefaultAddress("10", "Hovedgaden", "Vejle", "7100", "Danmark");

               await _customerRepository.AddAsync(customer);
            }

            
            string employeeEmail = "employee@test.local";
            var userEmployee = await _userRepository.GetByEmailAsync(employeeEmail, cancellationToken);
            if (userEmployee == null)
            {
                var employee = Employee.Create(
                    firstName: "Test",
                    lastName: "Medarbejder",
                    email: employeeEmail,
                    passwordHashed: _passwordHasher.HashPassword("ABCD1234"),
                    mfaEnabled: false,
                    role: Role.Employee,
                    department: Department.Sales,
                    permissions: null
                    );

                await _employeeRepository.AddAsync(employee);
            }

            string adminEmail = "admin@test.local";
            var adminEmployee = await _userRepository.GetByEmailAsync(adminEmail, cancellationToken);
            if (adminEmployee == null)
            {
                var employee = Employee.Create(
                    firstName: "Test",
                    lastName: "Medarbejder",
                    email: adminEmail,
                    passwordHashed: _passwordHasher.HashPassword("ABCD1234"),
                    mfaEnabled: false,
                    role: Role.Admin,
                    department: Department.Sales,
                    permissions: null
                    );

                await _employeeRepository.AddAsync(employee);
            }




            await dbContext.SaveChangesAsync();
        }

        
    }
}