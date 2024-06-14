using WebApplication1.DTOs;

namespace WebApplication1.Services;
using System.Threading.Tasks;

public interface IClientService
{
    Task<ClientDto> GetClientWithSubscriptionsAsync(int clientId);
}
