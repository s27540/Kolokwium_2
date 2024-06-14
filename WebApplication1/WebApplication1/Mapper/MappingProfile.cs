using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Mapper;


using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client, ClientDto>();
        CreateMap<Subscription, SubscriptionDto>();
        CreateMap<PaymentDto, Payment>();
    }
}
