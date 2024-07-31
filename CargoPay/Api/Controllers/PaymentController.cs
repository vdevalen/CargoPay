using CargoPay.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CargoPay.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("{cardId}/pay")]
        public async Task<IActionResult> Pay(int cardId, [FromBody] PaymentRequest request)
        {
            var success = await _paymentService.ProcessPaymentAsync(cardId, request.Amount);
            if (!success)
            {
                return BadRequest("Payment failed.");
            }

            return Ok("Payment successful.");
        }
    }

    public class PaymentRequest
    {
        public decimal Amount { get; set; }
    }
}
