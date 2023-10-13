
using MiniDefinition.Countries;
using MiniDefinition.Cities;
using MiniDefinition.ExchangeRateEntries;
using MiniDefinition.Currencies;
using AutoMapper;
using System.Linq;



namespace MiniDefinition

{
    public class MiniDefinitionApplicationAutoMapperProfile : Profile
    {
        public MiniDefinitionApplicationAutoMapperProfile()
        {

            CreateMap<Country, CountryDto>();
            CreateMap<City, CityDto>();
            CreateMap<ExchangeRateEntry, ExchangeRateEntryDto>();
            CreateMap<Currency, CurrencyDto>();
        }
    }

}