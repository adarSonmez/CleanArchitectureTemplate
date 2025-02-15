using AutoMapper;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Application.Features.Orders.Commands.CreateOrderFromBasket;
using CleanArchitectureTemplate.Domain.Entities.Ordering;

namespace CleanArchitectureTemplate.Application.Mappings.Ordering;

/// <summary>
/// AutoMapper profile for <see cref="Order"/> mapping."/>
/// </summary>
public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<CreateOrderFromBasketCommandRequest, Order>()
            .ForMember(dest => dest.Basket, opt => opt.Ignore());

        CreateMap<Order, OrderDto>();
    }
}