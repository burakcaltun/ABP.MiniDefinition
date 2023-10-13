using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using MiniDefinition.Domain.Entities;
using MiniDefinition.Domain.Services.Interfaces;
using MiniDefinition.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MiniDefinition.Domain.Services;

public class ExchangeRateEntryService : IExchangeRateEntryService
{
    protected readonly IExchangeRateEntryRepository _exchangeRateEntryRepository;

    public ExchangeRateEntryService(IExchangeRateEntryRepository exchangeRateEntryRepository)
    {
        _exchangeRateEntryRepository = exchangeRateEntryRepository;
    }

    public virtual async Task<ExchangeRateEntry> Save(ExchangeRateEntry exchangeRateEntry)
    {
        await _exchangeRateEntryRepository.CreateOrUpdateAsync(exchangeRateEntry);
        await _exchangeRateEntryRepository.SaveChangesAsync();
        return exchangeRateEntry;
    }

    public virtual async Task<IPage<ExchangeRateEntry>> FindAll(IPageable pageable)
    {
        var page = await _exchangeRateEntryRepository.QueryHelper()
            .Include(exchangeRateEntry => exchangeRateEntry.ExchangeRateEntry)
            .GetPageAsync(pageable);
        return page;
    }

    public virtual async Task<ExchangeRateEntry> FindOne(long id)
    {
        var result = await _exchangeRateEntryRepository.QueryHelper()
            .Include(exchangeRateEntry => exchangeRateEntry.ExchangeRateEntry)
            .GetOneAsync(exchangeRateEntry => exchangeRateEntry.Id == id);
        return result;
    }

    public virtual async Task Delete(long id)
    {
        await _exchangeRateEntryRepository.DeleteByIdAsync(id);
        await _exchangeRateEntryRepository.SaveChangesAsync();
    }
}
