using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using MiniDefinition.Domain.Entities;
using MiniDefinition.Domain.Services.Interfaces;
using MiniDefinition.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MiniDefinition.Domain.Services;

public class CurrencyService : ICurrencyService
{
    protected readonly ICurrencyRepository _currencyRepository;

    public CurrencyService(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public virtual async Task<Currency> Save(Currency currency)
    {
        await _currencyRepository.CreateOrUpdateAsync(currency);
        await _currencyRepository.SaveChangesAsync();
        return currency;
    }

    public virtual async Task<IPage<Currency>> FindAll(IPageable pageable)
    {
        var page = await _currencyRepository.QueryHelper()
            .GetPageAsync(pageable);
        return page;
    }

    public virtual async Task<Currency> FindOne(long id)
    {
        var result = await _currencyRepository.QueryHelper()
            .GetOneAsync(currency => currency.Id == id);
        return result;
    }

    public virtual async Task Delete(long id)
    {
        await _currencyRepository.DeleteByIdAsync(id);
        await _currencyRepository.SaveChangesAsync();
    }
}
