using AuthService.Entities;
using AuthService.Features.Shared.Auth;
using MediatR;
namespace AuthService.Features.CreateCustomer
{
    public record CreateCustomerCommand : IRequest<AuthDto>
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string Username { get; init; }
        public string? PhoneNumber { get; init; }
        public bool MfaEnabled { get; init; } = false;
        public string? UserAgent { get; init; }


    public CreateCustomerCommand(string firstName, string lastName, string email, string password, string username, string? phoneNumber = null, bool mfaEnabled = false, string? userAgent = null)
        {
            FirstName = firstName?.Trim() ?? string.Empty;
            LastName = lastName?.Trim() ?? string.Empty;
            Email = email?.Trim() ?? string.Empty;
            Password = password ?? string.Empty;
            Username = username?.Trim() ?? string.Empty;
            PhoneNumber = phoneNumber?.Trim();
            MfaEnabled = mfaEnabled;
            UserAgent = userAgent;
        }


    }
}
