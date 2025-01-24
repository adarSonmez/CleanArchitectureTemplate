using AutoMapper;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Features.Products.Commands.CreateProduct;
using CleanArchitectureTemplate.Domain.Entities.Shopping;

namespace CleanArchitectureTemplate.Application.Mappings.Shopping;

/// <summary>
/// AutoMapper profile for <see cref="Product"/> mapping."/>
/// </summary>
public class ProductImageFilesMappingProfile : Profile
{
    public ProductImageFilesMappingProfile()
    {
        CreateMap<CreateProductCommandRequest, Product>()
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            .ForMember(dest => dest.ProductImageFiles, opt => opt.Ignore())
            .ForMember(dest => dest.OrderItems, opt => opt.Ignore());

        CreateMap<Product, ProductDto>();
    }
}