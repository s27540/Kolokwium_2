using WebApplication1.Context;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class PaymentService : IPaymentService
{
    private readonly SubscriptionContext _context;
    private readonly IMapper _mapper;

    public PaymentService(SubscriptionContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> AddPaymentAsync(PaymentDto paymentDto)
    {
        var client = await _context.Clients.FindAsync(paymentDto.ClientId);
        if (client == null)
        {
            throw new Exception("Client not found");
        }

        var subscription = await _context.Subscriptions.FindAsync(paymentDto.SubscriptionId);
        if (subscription == null)
        {
            throw new Exception("Subscription not found");
        }

        if (subscription.EndDate < DateTime.Now)
        {
            throw new Exception("Subscription is not active");
        }

        var nextPaymentDate = subscription.CreatedAt.AddMonths(subscription.RenewalPeriod);
        if (DateTime.Now < nextPaymentDate)
        {
            throw new Exception("Payment not due yet");
        }

        var existingPayment = await _context.Payments
            .Where(p => p.ClientId == paymentDto.ClientId && p.SubscriptionId == paymentDto.SubscriptionId)
            .FirstOrDefaultAsync();

        if (existingPayment != null)
        {
            throw new Exception("Payment already exists for this period");
        }

        var discount = await _context.Discounts
            .Where(d => d.SubscriptionId == paymentDto.SubscriptionId && d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now)
            .OrderByDescending(d => d.Value)
            .FirstOrDefaultAsync();

        var amountToPay = subscription.TotalPaidAmount;
        if (discount != null)
        {
            amountToPay -= amountToPay * discount.Value / 100;
        }

        if (paymentDto.Amount != amountToPay)
        {
            throw new Exception("Incorrect payment amount");
        }

        var payment = _mapper.Map<Payment>(paymentDto);
        payment.PaymentDate = DateTime.Now;

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return payment.Id;
    }
}
