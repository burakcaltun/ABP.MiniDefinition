

using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using MiniDefinition.Shared;


namespace MiniDefinition.Cities.Interfaces

{
    public interface ICitiesAppService: IApplicationService
    {
        

        Task<PagedResultDto< CityDto >> GetListAsync(GetCitiesInput input);

        Task<CityDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CityDto> CreateAsync(CityCreateDto input);

        Task<CityDto> UpdateAsync(Guid id, CityUpdateDto input);
    }
}


