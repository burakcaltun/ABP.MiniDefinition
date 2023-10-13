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
    public class CurrencyRepository : GenericRepository<Currency, long>, ICurrencyRepository
    {
        public CurrencyRepository(IUnitOfWork context) : base(context)
        {
        }

        public override async Task<Currency> CreateOrUpdateAsync(Currency currency)
        {
            List<Type> entitiesToBeUpdated = new List<Type>();
            return await base.CreateOrUpdateAsync(currency, entitiesToBeUpdated);
        }
    }
}
