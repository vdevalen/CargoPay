using CargoPay.Domain.Entities;
using CargoPay.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CargoPay.Application.Services
{
    public class PaymentService
    {
        private readonly CardRepository _cardRepository;
        private readonly PaymentTransactionRepository _paymentTransactionRepository;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(CardRepository cardRepository, PaymentTransactionRepository paymentTransactionRepository, ILogger<PaymentService> logger)
        {
            _cardRepository = cardRepository;
            _paymentTransactionRepository = paymentTransactionRepository;
            _logger = logger;
        }

        public async Task<bool> ProcessPaymentAsync(int cardId, decimal amount)
        {
            _logger.LogInformation("Processing payment for card ID {CardId} with amount {Amount}", cardId, amount);

            var card = await _cardRepository.GetByIdAsync(cardId);
            if (card == null)
            {
                _logger.LogWarning("Card with ID {CardId} not found", cardId);
                return false;
            }

            if (card.Balance < amount)
            {
                _logger.LogWarning("Insufficient balance for card ID {CardId}. Current balance: {Balance}, attempted amount: {Amount}", cardId, card.Balance, amount);
                return false;
            }

            card.Balance -= amount;
            await _cardRepository.UpdateCardAsync(card);

            var paymentTransaction = new PaymentTransaction
            {
                CardId = cardId,
                Amount = amount,
                TransactionDate = DateTime.Now
            };

            await _paymentTransactionRepository.AddAsync(paymentTransaction);
            _logger.LogInformation("Payment processed successfully for card ID {CardId} with amount {Amount}", cardId, amount);
            return true;
        }
    }
}
