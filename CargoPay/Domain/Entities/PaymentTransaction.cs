using System;

namespace CargoPay.Domain.Entities
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } 
        public Card Card { get; set; }
    }
}