namespace Domain.Entities
{
    public class DeskEntity : NamedEntity
    {
        public IEnumerable<TaskEntity> Tasks { get; set; }
    }
}
