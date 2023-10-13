
using AutoMapper;
using System.Linq;
using MiniDefinition.Domain.Entities;
using MiniDefinition.Dto;


namespace MiniDefinition.Configuration.AutoMapper
{

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<ExchangeRateEntry, ExchangeRateEntryDto>().ReverseMap();
            CreateMap<Currency, CurrencyDto>().ReverseMap();
        }
    }
}
