using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartService.Features.AddToCart;
using ShoppingCartService.Features.ClearCart;
using ShoppingCartService.Features.GetCart;
using ShoppingCartService.Features.Shared;
using System.Security.Claims;

namespace ShoppingCartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public sealed record AddToCartRequest(Guid ProductId, int Quantity);
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("items")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request, CancellationToken ct)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdClaim, out var customerId))
            {
                return Unauthorized("Ugyldigt eller manglende bruger-ID i token.");
            }

            try
            {
                var command = new AddToCartCommand
                {
                    CustomerId = customerId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };

                await _mediator.Send(command, ct);
                return Ok();
            }
            catch (FluentValidation.ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Message = "Tilføjelse til kurv fejlede", Errors = errors });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<ShoppingCartDto>> GetCart(CancellationToken ct)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                              

            if (!Guid.TryParse(userIdClaim, out var customerId))
            {
                return Unauthorized("Ugyldigt eller manglende bruger-ID i token.");
            }

            try
            {
                var result = await _mediator.Send(new GetCartQuery(customerId), ct);
                return Ok(result);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Message = "Opslag af kurv fejlede", Errors = errors });
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ClearCart(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                              

            if (!Guid.TryParse(userIdClaim, out var customerId))
            {
                return Unauthorized("Ugyldigt eller manglende bruger-ID i token.");
            }

            try
            {
                await _mediator.Send(new ClearCartCommand(customerId), cancellationToken);
                return NoContent();
            }
            catch (FluentValidation.ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Message = "Rydning af kurv fejlede", Errors = errors });
            }
        }




    }
}
