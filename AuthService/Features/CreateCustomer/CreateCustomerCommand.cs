using AuthService.Entities;
using AuthService.Features.Shared.Customer;
using MediatR;
namespace AuthService.Features.CreateCustomer
{
    public record CreateCustomerCommand : IRequest<CustomerAuthDTO>
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string Username { get; init; }
        public string? PhoneNumber { get; init; }
        public bool MfaEnabled { get; init; } = false;
   
    public CreateCustomerCommand(string firstName, string lastName, string email, string password, string username, string? phoneNumber = null, bool mfaEnabled = false)
        {
            FirstName = firstName?.Trim() ?? string.Empty;
            LastName = lastName?.Trim() ?? string.Empty;
            Email = email?.Trim() ?? string.Empty;
            Password = password?.Trim() ?? string.Empty;
            Username = username?.Trim() ?? string.Empty;
            PhoneNumber = phoneNumber?.Trim();
            MfaEnabled = mfaEnabled;
        }


    }
}
