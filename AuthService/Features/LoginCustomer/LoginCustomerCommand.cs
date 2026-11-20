using AuthService.Features.Shared.Customer;
using MediatR;

namespace AuthService.Features.LoginCustomer
{
    public record LoginCustomerCommand(string Email, string Password) : IRequest<CustomerAuthDTO>;

    



}
