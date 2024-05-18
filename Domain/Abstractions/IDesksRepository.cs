using Domain.Entities;
using Models.Dto;
using System.Linq.Expressions;

namespace Domain.Abstractions
{
    public interface IDesksRepository : IRepository<DeskEntity, Guid>
    {

    }
}
