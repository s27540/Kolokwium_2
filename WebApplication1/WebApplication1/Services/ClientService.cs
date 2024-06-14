using WebApplication1.Context;
using WebApplication1.DTOs;

namespace WebApplication1.Services;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class ClientService : IClientService
{
    private readonly SubscriptionContext _context;
    private readonly IMapper _mapper;

    public ClientService(SubscriptionContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ClientDto> GetClientWithSubscriptionsAsync(int clientId)
    {
        var client = await _context.Clients
            .Include(c => c.Subscriptions)
            .FirstOrDefaultAsync(c => c.Id == clientId);

        if (client == null)
        {
            return null;
        }

        var clientDto = _mapper.Map<ClientDto>(client);
//        clientDto.Subscriptions = client.Subscriptions.Select(s => new SubscriptionDto
//        {
//            Id = s.Id,
//            Name = s.Name,
//            TotalPaidAmount = s.TotalPaidAmount
//        }).ToList();



        return clientDto;
    }
}
