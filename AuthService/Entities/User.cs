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
        public string PasswordHashed { get; private set; }
        public bool MfaEnabled { get; private set; }
        
        protected User() { } // For EF Core

        public User(Name name, string email, string passwordHashed, bool mfaEnabled)
        {
            UpdateName(name);
            UpdateEmail(email);
            UpdatePasswordHashed(passwordHashed);
            UpdateMfaEnabled(mfaEnabled);
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateName(Name name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
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

    }
}
