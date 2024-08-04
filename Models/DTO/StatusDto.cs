namespace Models.DTO
{
    public class StatusDto
    {
        /// <summary>
        /// Идентификатор статуса
        /// </summary>
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
