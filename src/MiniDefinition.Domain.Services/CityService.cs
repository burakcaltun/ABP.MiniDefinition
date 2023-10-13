using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using MiniDefinition.Domain.Entities;
using MiniDefinition.Domain.Services.Interfaces;
using MiniDefinition.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MiniDefinition.Domain.Services;

public class CityService : ICityService
{
    protected readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public virtual async Task<City> Save(City city)
    {
        await _cityRepository.CreateOrUpdateAsync(city);
        await _cityRepository.SaveChangesAsync();
        return city;
    }

    public virtual async Task<IPage<City>> FindAll(IPageable pageable)
    {
        var page = await _cityRepository.QueryHelper()
            .Include(city => city.Country)
            .GetPageAsync(pageable);
        return page;
    }

    public virtual async Task<City> FindOne(long id)
    {
        var result = await _cityRepository.QueryHelper()
            .Include(city => city.Country)
            .GetOneAsync(city => city.Id == id);
        return result;
    }

    public virtual async Task Delete(long id)
    {
        await _cityRepository.DeleteByIdAsync(id);
        await _cityRepository.SaveChangesAsync();
    }
}
