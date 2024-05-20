namespace Domain.Entities
{
    public class RoleEntity : NamedEntity
    {
        /// <summary>
        /// Описание роли
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Пользователи роли
        /// </summary>
        public IEnumerable<UserEntity> Users { get; set; }
    }
}
