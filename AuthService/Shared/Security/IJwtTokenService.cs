using AuthService.Features.Shared.Auth;
using System.Security.Claims;

namespace AuthService.Shared.Security
{
    public interface IJwtTokenService
    {
        string GenerateToken(AuthDto authDto);
       
  
    }
}
