using AutoMapper;
using PharmacyWebAPI.Models;
using PharmacyWebAPI.Models.ViewModels;

namespace PharmacyWebAPI.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDetailDto>()
                .ForMember(opt => opt.Quantity, src => src.Ignore())
                .ReverseMap();

            CreateMap<Product, ProductFormDto>()
                .ForMember(dest => dest.Categories, src => src.Ignore())
                .ForMember(dest => dest.Brands, src => src.Ignore())
                .ForMember(dest => dest.Storages, src => src.Ignore())
                .ReverseMap();
        }
    }
}