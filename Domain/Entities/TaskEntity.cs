using Domain.Abstractions;
using System.ComponentModel;

namespace Domain.Entities
{
    public class TaskEntity : IdEntity, IIsActive
    {
        public string Ticket { get; set; }
        public string Head { get; set; }
        public string Body { get; set; }
        public string Comment { get; set; }
        public Guid DeskId { get; set; }
        public DeskEntity Desk { get; set; }
        public Guid PerformerId { get; set; }
        public UserEntity Performer { get; set; }
        public Guid StatusId { get; set; }
        public StatusEntity Status { get; set; }
        public bool IsActive { get; set; }
    }
}
