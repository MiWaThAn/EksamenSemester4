using AuthService.Entities.ValueObjects;

namespace AuthService.Entities
{
    public class Customer : User
    {
        public string Username { get; private set; }
        public string PhoneNumber { get; private set; }
        //Birthday is optional, as it would be a part of discounts.
        public DateTime? Birthday {get; private set; }
        public Address? DefaultAddress { get; private set; }
        public PaymentMethodToken? PaymentInformation { get; private set; }
        
        protected Customer() { } // For EF Core

        private Customer(Name name, string email, string passwordHashed, bool mfaEnabled, string username, string phoneNumber, DateTime? birthday, Address? defaultAddress, PaymentMethodToken? paymentInformation)
            : base(name, email, passwordHashed, mfaEnabled)
        {
            UpdateUsername(username);
            UpdatePhoneNumber(phoneNumber);
            UpdateBirthday(birthday);
            UpdateDefaultAddress(defaultAddress);
            UpdatePaymentInformation(paymentInformation);
        }
        public static Customer Create(Name name, string email, string passwordHashed, bool mfaEnabled, string username, string phoneNumber, DateTime? birthday, Address? defaultAddress, PaymentMethodToken? paymentInformation)
        {
            return new Customer(name, email, passwordHashed, mfaEnabled, username, phoneNumber, birthday, defaultAddress, paymentInformation);
        }

        public void UpdateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty.", nameof(username));
            Username = username;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number cannot be empty.", nameof(phoneNumber));
            PhoneNumber = phoneNumber;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateBirthday(DateTime? birthday)
        {
            Birthday = birthday;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDefaultAddress(Address? defaultAddress)
        {
            DefaultAddress = defaultAddress;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePaymentInformation(PaymentMethodToken? paymentInformation)
        {
            PaymentInformation = paymentInformation;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
