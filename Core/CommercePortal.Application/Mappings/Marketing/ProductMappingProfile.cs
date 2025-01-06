using AutoMapper;
using CommercePortal.Application.Dtos.Marketing;
using CommercePortal.Application.Features.Products.Commands.CreateProduct;
using CommercePortal.Domain.Entities.Marketing;

namespace CommercePortal.Application.Mappings.Marketing;

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