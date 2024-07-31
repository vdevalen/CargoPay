using System.Threading.Tasks;

namespace CargoPay.Application.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(int cardId, decimal amount);
    }
}
