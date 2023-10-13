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
    public class CityRepository : GenericRepository<City, long>, ICityRepository
    {
        public CityRepository(IUnitOfWork context) : base(context)
        {
        }

        public override async Task<City> CreateOrUpdateAsync(City city)
        {
            List<Type> entitiesToBeUpdated = new List<Type>();
            return await base.CreateOrUpdateAsync(city, entitiesToBeUpdated);
        }
    }
}
