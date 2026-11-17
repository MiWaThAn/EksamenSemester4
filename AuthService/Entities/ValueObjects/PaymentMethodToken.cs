namespace AuthService.Entities.ValueObjects
{
    public record PaymentMethodToken
    {
        public string GatewayCustomerReferenceId { get; private set; } 
        public string CardProvider { get; private set; }
        public string LastFourDigits { get; private set; }

        protected PaymentMethodToken() { } // For EF Core
        private PaymentMethodToken(string gatewayCustomerReferenceId, string cardProvider, string lastFourDigits)
        {
            SetGatewayCustomerReferenceId(gatewayCustomerReferenceId);
            SetCardProvider(cardProvider);
            SetLastFourDigits(lastFourDigits);
        }
        public static PaymentMethodToken Create(string gatewayCustomerReferenceId, string cardProvider, string lastFourDigits)
        {
            return new PaymentMethodToken(gatewayCustomerReferenceId, cardProvider, lastFourDigits);
        }

        private void SetGatewayCustomerReferenceId(string gatewayCustomerReferenceId)
        {
            if (string.IsNullOrWhiteSpace(gatewayCustomerReferenceId))
                throw new ArgumentException("Gateway customer reference ID cannot be empty.", nameof(gatewayCustomerReferenceId));
            GatewayCustomerReferenceId = gatewayCustomerReferenceId;
        }
        private void SetCardProvider(string cardProvider)
        {
            if (string.IsNullOrWhiteSpace(cardProvider))
                throw new ArgumentException("Card provider cannot be empty.", nameof(cardProvider));
            CardProvider = cardProvider;
        }
        private void SetLastFourDigits(string lastFourDigits)
        {
            if (string.IsNullOrWhiteSpace(lastFourDigits))
                throw new ArgumentException("Last four digits cannot be empty.", nameof(lastFourDigits));
            LastFourDigits = lastFourDigits;
        }


    }
}
