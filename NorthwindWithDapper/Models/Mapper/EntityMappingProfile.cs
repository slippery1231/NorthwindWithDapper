using AutoMapper;
using NorthwindWithDapper.Models.Dtos;
using NorthwindWithDapper.Models.Entities;

namespace NorthwindWithDapper.Models.Mapper;

public class EntityMappingProfile : Profile
{
    public EntityMappingProfile()
    {
        CreateMap<Customers, CustomerDto>();
    }
}