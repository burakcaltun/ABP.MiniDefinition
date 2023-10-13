using DevExtreme.AspNet.Data.ResponseModel;
using MiniDefinition.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace MiniDefinition.Countries
{
    public interface ICountriesAppService : Interfaces.ICountriesAppService
    {
        Task<LoadResult> BPGetListCountry(string isActive, DataSourceLoadOptions loadOptions); //Getlist
        Task<CountryLookupDto> BPGetCountryByID(Guid? id); //GetById
        Task<List<LookupDto<int>>> BPPassiveStatus();
        Task<CountryDto> BPAddCountry(CountryDto input);
        Task<CountryDto> BPUpdateCountry(Guid id, CountryDto input);
        Task BPDeleteCountry(Guid id);
    }
}
