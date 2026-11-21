using AuthService.Features.Login;
using AuthService.Features.Logout;
using AuthService.Features.RefreshJwtToken;
using AuthService.Shared.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IMediator mediator, IJwtTokenService jwtTokenService)
        {
            _mediator = mediator;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var commandWithUserAgent = command with
                {
                    UserAgent = Request.Headers.UserAgent.ToString()
                };
                var authDto = await _mediator.Send(commandWithUserAgent, cancellationToken);
                var token = _jwtTokenService.GenerateToken(authDto);

                return Ok(new { token, refreshToken = authDto.RefreshToken });
            }
            catch (FluentValidation.ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Message = "Validering fejlede", Errors = errors });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshJwtTokenCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var commandWithUserAgent = command with
                {
                    UserAgent = Request.Headers.UserAgent.ToString()
                };

                var authDto = await _mediator.Send(commandWithUserAgent, cancellationToken);
                var token = _jwtTokenService.GenerateToken(authDto);

                return Ok(new { token, refreshToken = authDto.RefreshToken });
            }
            catch (FluentValidation.ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Message = "Validering fejlede", Errors = errors });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand command, CancellationToken cancellationToken)
        {
            try 
            { 
                bool loggedOut = await _mediator.Send(command, cancellationToken);
                if (loggedOut == true)
                {
                    return Ok(new { Message = "Du er logget ud" });
                }
                else
                {
                    return Ok(new { Message = "Du er logget ud" });
                }
            
            }
            catch (FluentValidation.ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Message = "Noget gik galt ved logout, prøv igen.", Errors = errors });
            }

        }

        
    }





}
