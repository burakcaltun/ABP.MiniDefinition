using MiniDefinition.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace MiniDefinition.ExchangeRateEntries
{
    public class EfCoreExchangeRateEntryRepository : Abstract.EfCoreExchangeRateEntryRepository , IExchangeRateEntryRepository
    {
        public EfCoreExchangeRateEntryRepository(IDbContextProvider<MiniDefinitionDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
