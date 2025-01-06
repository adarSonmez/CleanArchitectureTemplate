using AutoMapper;
using CommercePortal.Application.Dtos.Files;
using CommercePortal.Domain.Entities.Files;

namespace CommercePortal.Application.Mappings.Files;

/// <summary>
/// AutoMapper profile for <see cref="ProductImageFile"/> mapping."/>
/// </summary>
public class ProductImageFilesMappingProfile : Profile
{
    public ProductImageFilesMappingProfile()
    {
        CreateMap<ProductImageFile, ProductImageFileDto>();
    }
}