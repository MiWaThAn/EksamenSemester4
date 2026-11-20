using AuthService.Data;
using AuthService.Features.Shared.Customer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Features.LoginCustomer
{
    public class LoginCustomerCommandHandler : IRequestHandler<LoginCustomerCommand, CustomerAuthDTO>
    {
        private readonly AuthContext _context;

        public LoginCustomerCommandHandler(AuthContext context)
        {
            _context = context;
        }

        public async Task<CustomerAuthDTO> Handle(LoginCustomerCommand request, CancellationToken cancellationToken)
        {
            var normalizedEmail = request.Email.Trim().ToLowerInvariant();

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email.ToLower() == normalizedEmail, cancellationToken);

            if (customer is null || !BCrypt.Net.BCrypt.Verify(request.Password, customer.PasswordHashed))
            {
                throw new UnauthorizedAccessException("Ugyldig e-mail eller adgangskode.");
            }

            return new CustomerAuthDTO(
                customer.Id,
                customer.Email,
                customer.Role.ToString()
            );
        }
    }
}