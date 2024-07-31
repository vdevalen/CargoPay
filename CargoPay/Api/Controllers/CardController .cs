using CargoPay.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CargoPay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly CardService _cardService;

        public CardController(CardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] CreateCardRequest request)
        {
            var createdCard = await _cardService.CreateCardAsync(request.CardNumber, request.InitialBalance);
            return CreatedAtAction(nameof(GetCardByNumber), new { cardNumber = createdCard.CardNumber }, createdCard);
        }

        [HttpGet("{cardNumber}")]
        public async Task<IActionResult> GetCardByNumber(string cardNumber)
        {
            var card = await _cardService.GetCardByNumberAsync(cardNumber);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        [HttpGet("{cardNumber}/balance")]
        public async Task<IActionResult> GetCardBalance(string cardNumber)
        {
            var card = await _cardService.GetCardByNumberAsync(cardNumber);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card.Balance);
        }
    }

    public class CreateCardRequest
    {
        public string CardNumber { get; set; }
        public decimal InitialBalance { get; set; }
    }
}
