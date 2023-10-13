using MiniDefinition.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace MiniDefinition.Cities
{
    public class EfCoreCityRepository : Abstract.EfCoreCityRepository, ICityRepository
    {
        public EfCoreCityRepository(IDbContextProvider<MiniDefinitionDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
