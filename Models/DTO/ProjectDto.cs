namespace Models.Dto
{
    public class ProjectDto
    {
        /// <summary>
        /// Идентификатор проекта
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Наименование проекта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание проекта
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата начала проекта
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания проекта
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Статус проекта 
        /// </summary>
        public string Status { get; set; } //TODO ENUM

        /// <summary>
        /// Владелец проекта
        /// </summary>
        public UserDto Owner { get; set; }

        /// <summary>
        /// Пользователи проекта
        /// </summary>
        public IEnumerable<UserDto> Users { get; set; }

        /// <summary>
        /// Доски проекта
        /// </summary>
        public IEnumerable<DeskDto> Desks { get; set; }
    }
}
