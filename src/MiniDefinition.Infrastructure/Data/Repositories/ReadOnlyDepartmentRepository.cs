using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JHipsterNet.Core.Pagination;
using JHipsterNet.Core.Pagination.Extensions;
using MiniDefinition.Domain.Entities;
using MiniDefinition.Domain.Repositories.Interfaces;
using MiniDefinition.Infrastructure.Data.Extensions;

namespace MiniDefinition.Infrastructure.Data.Repositories
{
    public class ReadOnlyDepartmentRepository : ReadOnlyGenericRepository<Department, long>, IReadOnlyDepartmentRepository
    {
        public ReadOnlyDepartmentRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
