using Domain.Abstractions;

namespace Domain.Entities
{
    public class TaskEntity : IdEntity, IIsActive
    {
        public string Ticket { get; set; }
        public string Head { get; set; }
        public string Body { get; set; }
        public string Comment { get; set; }
        public DeskEntity Desk { get; set; }
        public UserEntity Performer { get; set; }
        public bool IsActive { get; set; }
    }
}
