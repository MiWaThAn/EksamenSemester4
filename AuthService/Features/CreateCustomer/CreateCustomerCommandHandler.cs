using AuthService.Entities;
using AuthService.Entities.ValueObjects;
using AuthService.Features.Shared.Auth;
using AuthService.Features.Shared.Auth.RefreshTokenHelper;
using AuthService.Repositories;
using AuthService.Shared.Security;
using MediatR;

namespace AuthService.Features.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, AuthDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenHelper _refreshTokenHelper;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IPasswordHasher passwordHasher, IRefreshTokenHelper refreshTokenHelper)
        {
            _customerRepository = customerRepository;
            _passwordHasher = passwordHasher;
            _refreshTokenHelper = refreshTokenHelper;
        }

        public async Task<AuthDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            
            

            var existingCustomer = await _customerRepository.GetByEmailAsync(request.Email, cancellationToken);

            //I should probably change this to a result instead of throwing an exception, will be done if I have time.
            if (existingCustomer != null)
                throw new InvalidOperationException("Der findes allerede en kunde med denne email");

            var hashedPassword =  _passwordHasher.HashPassword(request.Password);

            
            var customer = Customer.Create(
                firstName: request.FirstName,
                lastName: request.LastName,
                email: request.Email,
                passwordHashed: hashedPassword,
                mfaEnabled: request.MfaEnabled,
                username: request.Username,
                phoneNumber: request.PhoneNumber
            );

            
            await _customerRepository.AddAsync(customer);

            await _customerRepository.SaveChangesAsync();

            var refreshToken = await _refreshTokenHelper.CreateOrUpdateRefreshToken(
    customer.Id,
    request.UserAgent ?? string.Empty,
    cancellationToken);

            return new AuthDto(
                Id: customer.Id,
                Email: customer.Email,
                Role: customer.Role.ToString(),
                Permissions: null,
                RefreshToken: refreshToken
            );
        }
    }
}