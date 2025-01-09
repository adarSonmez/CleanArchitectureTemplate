using AutoMapper;
using CleanArchitectureTemplate.Application.Dtos.Marketing;
using CleanArchitectureTemplate.Application.Features.Products.Commands.CreateProduct;
using CleanArchitectureTemplate.Domain.Entities.Marketing;

namespace CleanArchitectureTemplate.Application.Mappings.Marketing;

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
            .ForMember(dest => dest.Orders, opt => opt.Ignore());

        CreateMap<Product, ProductDto>();
    }
}