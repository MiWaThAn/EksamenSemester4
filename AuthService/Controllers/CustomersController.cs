using AuthService.Features.CreateCustomer;
using AuthService.Features.DeleteCustomer;
using AuthService.Features.LoginCustomer;
using AuthService.Features.UpdateCustomerProfile;
using AuthService.Shared.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtTokenService _jwtTokenService;

        public CustomersController(IMediator mediator, IJwtTokenService jwtTokenService)
        {
            _mediator = mediator;
            _jwtTokenService = jwtTokenService;
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateCustomerCommand command)
        {
            try
            {
                var customerAuthDTO = await _mediator.Send(command);
                var token = _jwtTokenService.GenerateCustomerToken(customerAuthDTO);

                return StatusCode(StatusCodes.Status201Created, new { token });
            }
            catch (FluentValidation.ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Message = "Validering fejlede", Errors = errors });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCustomerCommand command)
        {
            try
            {
                var customerAuthDTO = await _mediator.Send(command);
                var token = _jwtTokenService.GenerateCustomerToken(customerAuthDTO);

                return Ok(new { token });
            }
            catch (FluentValidation.ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Message = "Validering fejlede", Errors = errors });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut("{id:guid}/profile")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UpdateCustomerProfileCommand request, CancellationToken cancellationToken)
        {
            if (!IsSameUser(id))
            {
                return Forbid();
            }
            try
            {
                var updated = await _mediator.Send(request, cancellationToken);
                if (!updated)
                {
                    return NotFound(new { Message = $"Kunde med ID '{id}' blev ikke fundet." });
                }

                return NoContent();
            }
            catch (FluentValidation.ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Message = "Validering fejlede", Errors = errors });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {

            if (!IsSameUser(id))
            {
                return Forbid();
            }

            try
            {
                var deleted = await _mediator.Send(new DeleteCustomerCommand(id), cancellationToken);
                if (!deleted)
                {
                    return NotFound(new { Message = $"Kunde med ID '{id}' blev ikke fundet." });
                }

                return NoContent();
            }
            catch (FluentValidation.ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Message = "Validering fejlede", Errors = errors });
            }
        }


        private bool IsSameUser(Guid targetUserId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userIdClaim, out var currentUserId) && currentUserId == targetUserId;
        }






    }
}
