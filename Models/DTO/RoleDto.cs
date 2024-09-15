namespace Models.Dto
{
    public class RoleDto
    {
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Наименование роли
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание роли
        /// </summary>
        public string Description { get; set; } 
    }
}
