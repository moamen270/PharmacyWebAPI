using AutoMapper;
using PharmacyWebAPI.Models;
using PharmacyWebAPI.Models.Dto;

namespace PharmacyWebAPI.Utility.Setting
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Drug, DrugGetDto>()
                .ReverseMap()
                .ForMember(dest => dest.Category, src => src.Ignore())
                .ForMember(dest => dest.Manufacturer, src => src.Ignore())
                .ForMember(dest => dest.ManufacturerName, src => src.Ignore())
                .ForMember(dest => dest.CategoryName, src => src.Ignore());

            CreateMap<Drug, DrugDetailsGetDto>()
                .ReverseMap()
                .ForMember(dest => dest.ManufacturerName, src => src.Ignore())
                .ForMember(dest => dest.CategoryName, src => src.Ignore());

            CreateMap<Drug, PostDrugDto>()
                .ForMember(dest => dest.Categories, src => src.Ignore())
                .ForMember(dest => dest.Manufacturers, src => src.Ignore())
                .ReverseMap();

            CreateMap<Order, OrderDto>()
                .ReverseMap()
                .ForMember(dest => dest.SessionId, src => src.Ignore());

            CreateMap<OrderDetail, OrderDetailsDto>()
               .ReverseMap()
               .ForMember(dest => dest.Drug, src => src.Ignore())
               .ForMember(dest => dest.Order, src => src.Ignore())
               .ForMember(dest => dest.OrderId, src => src.Ignore());
        }
    }
}