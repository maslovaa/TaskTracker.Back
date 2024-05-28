using Domain.Abstractions;

namespace Domain.Entities
{
    public class DeskEntity : NamedEntity, IIsActive
    {
        public ProjectEntity Project { get; set; }
        public IEnumerable<TaskEntity> Tasks { get; set; }
        public bool IsActive { get; set; }
    }
}
