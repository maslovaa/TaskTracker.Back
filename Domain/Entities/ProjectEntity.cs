namespace Domain.Entities
{
    public class ProjectEntity : NamedEntity
    {
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
        /// Владелецы проекта
        /// </summary>
        public IEnumerable<UserEntity> Users { get; set; }

        /// <summary>
        /// Задачи проекта
        /// </summary>
        public IEnumerable<TaskEntity> Tasks { get; set; }
        public IEnumerable<DeskEntity> Desks { get; set; }
    }
}
