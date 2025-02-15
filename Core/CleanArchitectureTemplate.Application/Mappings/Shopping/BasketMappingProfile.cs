using AutoMapper;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;

namespace CleanArchitectureTemplate.Application.Mappings.Shopping;

/// <summary>
/// AutoMapper profile for <see cref="Basket"/> mapping."/>
/// </summary>
public class BasketMappingProfile : Profile
{
    public BasketMappingProfile()
    {
        CreateMap<BasketItem, BasketItemDto>();
    }
}