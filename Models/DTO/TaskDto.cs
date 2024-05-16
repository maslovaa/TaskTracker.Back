namespace Models.Dto
{
    public class TaskDto
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public Guid? Id { get; set; }
        public string Ticket { get; set; }
        public string Head { get; set; }
        public string Body { get; set; }
        public string Comment { get; set; }
    }
}
