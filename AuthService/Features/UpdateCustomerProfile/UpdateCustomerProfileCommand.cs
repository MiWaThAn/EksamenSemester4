using MediatR;

namespace AuthService.Features.UpdateCustomerProfile
{
    public record UpdateCustomerProfileCommand : IRequest<bool>
    { 
       public Guid Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string? PhoneNumber { get; init; }
        public string? StreetNumber { get; init; }
        public string? Street { get; init; }
        public string? City { get; init; }
        public string? ZipCode { get; init; }
        public string? Country { get; init; }

        public UpdateCustomerProfileCommand(Guid id, string firstName, string lastName, string email, string phoneNumber, string streetNumber, string street, string city, string zipCode, string country)
        {
            Id = id;
            FirstName = firstName?.Trim() ?? string.Empty;
            LastName = lastName?.Trim() ?? string.Empty;
            Email = email?.Trim() ?? string.Empty;
            PhoneNumber = phoneNumber?.Trim();
            StreetNumber = streetNumber?.Trim();
            Street = street?.Trim();
            City = city?.Trim();
            ZipCode = zipCode?.Trim();
            Country = country?.Trim();
        }

       
}
}
