using Domain.Entities;
using Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IDesksRepository
    {
        /// <summary>
        /// Получение доски по идентификатору
        /// </summary>
        /// <param name="deskId">Идентификатор доски</param>
        /// <returns></returns>
        Task<DeskDto> FindDeskByIdAsync(Guid deskId);

        /// <summary>
        /// Получение коллекции досок по переданному выражению
        /// </summary>
        /// <param name="predicate">Выражение филтра для отбора сущностей</param>
        /// <returns></returns>
        Task<List<DeskDto>> GetDesksByPredicateAsync(Expression<Func<DeskEntity, bool>> predicate);

        /// <summary>
        /// Добавление доски
        /// </summary>
        /// <param name="deskDto">Входной объект доски</param>
        /// <returns></returns>
        Task<Guid> AddDeskAsync(DeskDto deskDto);

        /// <summary>
        /// Обновление доски
        /// </summary>
        /// <param name="deskDto">Входной объект доски</param>
        /// <returns></returns>
        Task<bool> UpdateDeskAsync(DeskDto deskDto);

        /// <summary>
        /// Удаление доски
        /// </summary>
        /// <param name="deskId">Идентификатор доски</param>
        /// <returns></returns>
        Task<bool> DeleteDeskAsync(Guid deskId);
    }
}
