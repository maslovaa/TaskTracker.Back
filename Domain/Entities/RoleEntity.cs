using Domain.Abstractions;

namespace Domain.Entities
{
    public class RoleEntity : NamedEntity, IIsActive
    {
        /// <summary>
        /// Описание роли
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Пользователи роли
        /// </summary>
        public IEnumerable<UserEntity> Users { get; set; }

        /// <summary>
        /// Признак активности
        /// </summary>
        public bool IsActive { get; set; }
    }
}
