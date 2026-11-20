using AuthService.Entities.Enums;
using AuthService.Entities.ValueObjects;
namespace AuthService.Entities
{
    public class Customer : User
    {
        public string Username { get; private set; }
        public string? PhoneNumber { get; private set; }
        //Birthday is optional, as it would be a part of discounts.
        public DateTime? Birthday {get; private set; }
        public Address? DefaultAddress { get; private set; }
        public PaymentMethodToken? PaymentInformation { get; private set; }
        
        protected Customer() { } // For EF Core

        private Customer(string firstName, string lastName, string email, string passwordHashed, bool mfaEnabled, string username, string? phoneNumber)
            : base(firstName, lastName, email, passwordHashed, mfaEnabled, Role.Customer)
        {
            UpdateUsername(username);
            UpdatePhoneNumber(phoneNumber);
        }

        public static Customer Create(string firstName, string lastName, string email, string passwordHashed, bool mfaEnabled, string username, string? phoneNumber)
        {
            return new Customer(firstName, lastName, email, passwordHashed, mfaEnabled, username, phoneNumber);
        }

        public void UpdateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty.", nameof(username));
            Username = username;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePhoneNumber(string? phoneNumber)
        {
            PhoneNumber = phoneNumber;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateBirthday(DateTime? birthday)
        {
            Birthday = birthday;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDefaultAddress(string streetNumber, string street, string city, string zipCode, string country)
        {
            DefaultAddress = Address.Create(streetNumber,street, city, zipCode, country);
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePaymentInformation(PaymentMethodToken? paymentInformation)
        {
            PaymentInformation = paymentInformation;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void ClearDefaultAddress()
        {
            DefaultAddress = null;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
