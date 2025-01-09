using AutoMapper;
using CleanArchitectureTemplate.Application.Dtos.Files;
using CleanArchitectureTemplate.Domain.Entities.Files;

namespace CleanArchitectureTemplate.Application.Mappings.Files;

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