
using Microsoft.EntityFrameworkCore;
using MiniDefinition.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace MiniDefinition.Countries
{
    public class EfCoreCountryRepository : Abstract.EfCoreCountryRepository, ICountryRepository
    {
        public EfCoreCountryRepository(IDbContextProvider<MiniDefinitionDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<Country> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(country => country.Name == name);
        }
    }
}
