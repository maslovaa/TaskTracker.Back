namespace Models.Dto
{
    public class DeskDto
    {
        /// <summary>
        /// Идентификатор доски
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Наименование доски
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Задачи доски
        /// </summary>
        public IEnumerable<TaskDto> Tasks { get; set; }
    }
}
