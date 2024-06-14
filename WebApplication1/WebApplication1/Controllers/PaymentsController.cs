using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddPayment(PaymentDto paymentDto)
    {
        try
        {
            var paymentId = await _paymentService.AddPaymentAsync(paymentDto);
            return Ok(paymentId);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
