using MediatR;

namespace AuthService.Features.Logout
{
    public record LogoutCommand(string refreshToken) : IRequest<bool>;
    
}
