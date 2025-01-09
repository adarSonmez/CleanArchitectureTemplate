/**
 * NOTE TO DEVELOPERS:
 *
 * This is the only mapper profile that is located in the Persistence project.
 * This is because it is responsible for mapping between the domain entities and the identity entities,
 * which are specific to the infrastructure layer.
 * Other mapping profiles are located in the Application project to ensure that the domain layer remains isolated from infrastructure concerns.
*/

using AutoMapper;
using CleanArchitectureTemplate.Domain.Entities.Identity;

namespace CleanArchitectureTemplate.Persistence.Identity;

/// <summary>
/// Represents the mapping profile for the identity entities.
/// </summary>
public class IdentityMappingProfile : Profile
{
    public IdentityMappingProfile()
    {
        // User Mapping
        CreateMap<DomainUser, AppUser>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName ?? string.Empty));

        CreateMap<AppUser, DomainUser>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.FullName) ? null : src.FullName));

        // Role Mapping
        CreateMap<DomainRole, AppRole>();
        CreateMap<AppRole, DomainRole>();
    }
}