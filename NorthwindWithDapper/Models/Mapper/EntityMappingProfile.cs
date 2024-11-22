using AutoMapper;
using NorthwindWithDapper.Models.Dtos;
using NorthwindWithDapper.Models.Entities;
using NorthwindWithDapper.Models.ViewModel;

namespace NorthwindWithDapper.Models.Mapper;

public class EntityMappingProfile : Profile
{
    public EntityMappingProfile()
    {
        CreateMap<Customers, CustomerDto>();
        CreateMap<CustomerViewModel, Customers>();
    }
}