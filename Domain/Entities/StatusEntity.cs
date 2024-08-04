using Domain.Abstractions;

namespace Domain.Entities
{
    public class StatusEntity : NamedEntity, IIsActive
    {
        /// <summary>
        /// Описание статуса
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Признак активности
        /// </summary>
        public bool IsActive { get; set; }
    }
}
