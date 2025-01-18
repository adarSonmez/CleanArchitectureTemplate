using AutoMapper;
using CleanArchitectureTemplate.Application.Dtos.Marketing;
using CleanArchitectureTemplate.Domain.Entities.Marketing;

namespace CleanArchitectureTemplate.Application.Mappings.Marketing;

/// <summary>
/// AutoMapper profile for <see cref="Category"/> mapping."/>
/// </summary>
public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>();
    }
}