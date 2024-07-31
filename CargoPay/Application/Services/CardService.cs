using CargoPay.Domain.Entities;
using CargoPay.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace CargoPay.Application.Services
{
    public class CardService
    {
        private readonly CardRepository _cardRepository;

        public CardService(CardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<Card> CreateCardAsync(string cardNumber, decimal initialBalance)
        {
            var card = new Card
            {
                CardNumber = cardNumber,
                Balance = initialBalance
            };

            await _cardRepository.AddAsync(card);
            return card;
        }

        public async Task<Card> GetCardByNumberAsync(string cardNumber)
        {
            return await _cardRepository.GetByNumberAsync(cardNumber);
        }
    }
}