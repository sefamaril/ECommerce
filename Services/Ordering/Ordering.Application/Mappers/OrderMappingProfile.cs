using AutoMapper;
using EventBus.Message.Events;
using Ordering.Application.Commands;
using Ordering.Application.Responses;
using Ordering.Core.Entities;

namespace Ordering.Application.Mappers;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderResponse>().ReverseMap();
        CreateMap<Order, CheckOutOrderCommand>().ReverseMap();
        CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        CreateMap<Order, CheckOutOrderCommandV2>().ReverseMap();
        CreateMap<CheckOutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        CreateMap<CheckOutOrderCommandV2, BasketCheckoutEventV2>().ReverseMap();
    }
}