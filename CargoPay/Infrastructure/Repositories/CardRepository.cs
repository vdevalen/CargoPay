using CargoPay.Domain.Entities;
using CargoPay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CargoPay.Infrastructure.Repositories
{
    public class CardRepository
    {
        private readonly ApplicationDbContext _context;

        public CardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Card card)
        {
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
        }

        public async Task<Card> GetByNumberAsync(string cardNumber)
        {
            return await _context.Cards.SingleOrDefaultAsync(c => c.CardNumber == cardNumber);
        }

        public async Task<Card> GetByIdAsync(int id)
        {
            return await _context.Cards.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateCardAsync(Card card)
        {
            _context.Cards.Update(card);
            await _context.SaveChangesAsync();
        }
    }
}
