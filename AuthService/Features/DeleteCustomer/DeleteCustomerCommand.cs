using MediatR;

namespace AuthService.Features.DeleteCustomer
{
    public record DeleteCustomerCommand(Guid Id) : IRequest<bool>;
}
