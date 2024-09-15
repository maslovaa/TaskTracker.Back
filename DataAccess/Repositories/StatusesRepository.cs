using Domain.Abstractions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class StatusesRepository : Repository<StatusEntity, Guid>, IStatusesRepository
    {
        public StatusesRepository(DataContext context) : base(context) { }
    }
}
