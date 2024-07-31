using System.Collections.Generic;

namespace CargoPay.Domain.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public decimal Balance { get; set; }
        public List<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();
    }
}