using DevExtreme.AspNet.Data.ResponseModel;
using MiniDefinition.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace MiniDefinition.Cities
{
    public interface ICitiesAppService : Interfaces.ICitiesAppService
    {
        Task<LoadResult> BPGetListCity(string? isActive, DataSourceLoadOptions loadOptions);
        Task<CityLookupDto> BPGetCityByID(Guid? id);
        Task<PagedResultDto<LookupDto<Guid>>> GetCountryLookupAsync(LookupRequestDto input);
 
        Task<List<LookupDto<int>>> BPPassiveStatus();
        Task<CityDto> BPAddCity(CityDto input);
        Task<CityDto> BPUpdateCity(Guid id, CityDto input);
        Task BPDeleteCity(Guid id);
    }
}
