namespace Domain.Entities
{
    public class DeskEntity : NamedEntity
    {
        public ProjectEntity Project { get; set; }
        public IEnumerable<TaskEntity> Tasks { get; set; }
    }
}
