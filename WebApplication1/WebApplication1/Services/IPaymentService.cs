using WebApplication1.DTOs;

namespace WebApplication1.Services;
using System.Threading.Tasks;

public interface IPaymentService
{
    Task<int> AddPaymentAsync(PaymentDto paymentDto);
}
