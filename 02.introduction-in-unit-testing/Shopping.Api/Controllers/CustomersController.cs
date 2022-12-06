using Microsoft.AspNetCore.Mvc;
using Shopping.Business.Carts;

namespace Shopping.Api.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}/cart")]
    public class CustomersController : ControllerBase
    {
        private readonly CartService _cartService;

        public CustomersController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult FindCustomerCart([FromRoute] string customerId) 
            => Ok(_cartService.FindCustomerCart(customerId));

        [HttpPost("lines")]
        public IActionResult AddNewLine([FromRoute] string customerId, [FromBody] NewCartLineModel line)
        {
            _cartService.AddNewLine(customerId, line);

            return NoContent();
        }
    }
}