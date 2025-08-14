using AutoMapper;
using LuxuryHub.Application.DTOs;
using LuxuryHub.Application.Requests;
using LuxuryHub.Domain.Entities;

namespace LuxuryHub.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Owner mappings
        CreateMap<CreateOwnerRequest, Owner>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        
        CreateMap<Owner, OwnerDto>();

        // Property mappings
        CreateMap<CreatePropertyRequest, Property>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        
        CreateMap<Property, PropertyDto>()
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.MainImage));

        // PropertyImage mappings
        CreateMap<CreatePropertyImageRequest, PropertyImage>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        
        CreateMap<PropertyImage, PropertyImageDto>();

        // PropertyTrace mappings
        CreateMap<CreatePropertyTraceRequest, PropertyTrace>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        
        CreateMap<PropertyTrace, PropertyTraceDto>();
    }
}
