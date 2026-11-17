namespace AuthService.Entities.ValueObjects
{
    public record Name
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName => $"{FirstName} {LastName}";

        protected Name() { } // For EF Core
        private Name(string firstName, string lastName)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
        }
        
        public static Name Create(string firstName, string lastName)
        {
            return new Name(firstName, lastName);
        }

        private void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            FirstName = firstName;
        }

        private void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
            LastName = lastName;
        }

    }
}
