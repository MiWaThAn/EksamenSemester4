namespace AuthService.Entities.ValueObjects
{
    public record Address
    {
        public string StreetNumber { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string Country { get; private set; }
    
    
        protected Address() { } // For EF Core

        private Address(string streetNumber, string street, string city, string zipCode, string country)
        {
            SetStreetNumber(streetNumber);
            SetStreet(street);
            SetCity(city);
            SetZipCode(zipCode);
            SetCountry(country);
        }

        public static Address Create(string streetNumber, string street, string city, string zipCode, string country)
        {
            return new Address(streetNumber, street, city, zipCode, country);
        }

        private void SetStreetNumber(string streetNumber)
        {
            if (string.IsNullOrWhiteSpace(streetNumber))
                throw new ArgumentException("Street number cannot be empty.", nameof(streetNumber));
            StreetNumber = streetNumber;
        }

        private void SetStreet(string street)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street cannot be empty.", nameof(street));
            Street = street;
        }

        private void SetCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty.", nameof(city));
            City = city;
        }

        private void SetZipCode(string zipCode)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ArgumentException("Zip code cannot be empty.", nameof(zipCode));
            ZipCode = zipCode;
        }

        private void SetCountry(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country cannot be empty.", nameof(country));
            Country = country;
        }

    }
}
