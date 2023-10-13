using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JHipsterNet.Core.Pagination;
using JHipsterNet.Core.Pagination.Extensions;
using MiniDefinition.Domain.Entities;
using MiniDefinition.Domain.Repositories.Interfaces;
using MiniDefinition.Infrastructure.Data.Extensions;

namespace MiniDefinition.Infrastructure.Data.Repositories
{
    public class ExchangeRateEntryRepository : GenericRepository<ExchangeRateEntry, long>, IExchangeRateEntryRepository
    {
        public ExchangeRateEntryRepository(IUnitOfWork context) : base(context)
        {
        }

        public override async Task<ExchangeRateEntry> CreateOrUpdateAsync(ExchangeRateEntry exchangeRateEntry)
        {
            List<Type> entitiesToBeUpdated = new List<Type>();
            return await base.CreateOrUpdateAsync(exchangeRateEntry, entitiesToBeUpdated);
        }
    }
}
