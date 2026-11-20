using AuthService.Repositories;
using MediatR;

namespace AuthService.Features.UpdateCustomerProfile
{
    public class UpdateCustomerProfileCommandHandler : IRequestHandler<UpdateCustomerProfileCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerProfileCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(UpdateCustomerProfileCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);
            if (customer == null)
            {
                return false;
            }

            // I Should probably seperate out email in a future iteration, as it might affect more than should be handled in one command.
            customer.UpdateEmail(request.Email);

            customer.UpdateName(request.FirstName, request.LastName);
            customer.UpdatePhoneNumber(request.PhoneNumber);
            if (!string.IsNullOrWhiteSpace(request.StreetNumber) && !string.IsNullOrWhiteSpace(request.Street) && !string.IsNullOrWhiteSpace(request.City) && !string.IsNullOrWhiteSpace(request.ZipCode) && !string.IsNullOrWhiteSpace(request.Country)) 
    {
                customer.UpdateDefaultAddress(request.StreetNumber, request.Street, request.City, request.ZipCode, request.Country);
            }
            else if( !string.IsNullOrWhiteSpace(request.StreetNumber) || !string.IsNullOrWhiteSpace(request.Street) || !string.IsNullOrWhiteSpace(request.City) ||!string.IsNullOrWhiteSpace(request.ZipCode) ||!string.IsNullOrWhiteSpace(request.Country))
            {
                throw new ArgumentException("Hvis du vil opdatere adressen, skal alle adressefelter udfyldes.");
            }
            else
            {
                customer.ClearDefaultAddress();
            }
            await _customerRepository.UpdateAsync(customer);
            await _customerRepository.SaveChangesAsync();

            return true;
        }
    }
}