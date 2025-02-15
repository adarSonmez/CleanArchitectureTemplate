using AutoMapper;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Features.BasketItems.Commands.AddBasketItemToBasket;
using CleanArchitectureTemplate.Domain.Entities.Shopping;

namespace CleanArchitectureTemplate.Application.Mappings.Shopping;

/// <summary>
/// AutoMapper profile for <see cref="BasketItem"/> mapping."/>
/// </summary>
public class BasketItemMappingProfile : Profile
{
    public BasketItemMappingProfile()
    {
        CreateMap<AddBasketItemToBasketCommandRequest, BasketItem>()
            .ForMember(dest => dest.Basket, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore());

        CreateMap<BasketItem, BasketItemDto>();
    }
}