using AuthService.Entities.Enums;
using AuthService.Entities.ValueObjects;

namespace AuthService.Entities
{
    public abstract class User
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; protected set; } = null;
        public Name Name { get; private set; }
        public string Email { get; private set; }
        public Role Role { get; protected set; }
        public string PasswordHashed { get; private set; }
        public bool MfaEnabled { get; private set; }

        
        protected User() { } // For EF Core

        public User(string firstName, string lastName, string email, string passwordHashed, bool mfaEnabled, Role role)
        {
            UpdateName(firstName, lastName);
            UpdateEmail(email);
            UpdatePasswordHashed(passwordHashed);
            UpdateMfaEnabled(mfaEnabled);
            UpdateRole(role);
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateName(string firstName, string lastName)
        {
            Name = Name.Create(firstName, lastName);
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));
            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePasswordHashed(string passwordHashed)
        {
            if (string.IsNullOrWhiteSpace(passwordHashed))
                throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHashed));
            PasswordHashed = passwordHashed;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateMfaEnabled(bool mfaEnabled)
        {
            MfaEnabled = mfaEnabled;
            UpdatedAt = DateTime.UtcNow;
        }
        
        private void UpdateRole(Role role)
        {
            
            Role = role;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
