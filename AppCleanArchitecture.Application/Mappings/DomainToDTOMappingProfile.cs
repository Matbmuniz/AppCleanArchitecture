using AppCleanArchitecture.Application.DTOs;
using AppCleanArchitecture.Domain.Entities;
using AutoMapper;

namespace AppCleanArchitecture.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
