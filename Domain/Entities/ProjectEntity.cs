﻿namespace Domain.Entities
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
        /// Владельцы проекта
        /// </summary>
        public IEnumerable<UserEntity> Owners { get; set; }

        /// <summary>
        /// Доски проекта
        /// </summary>
        public IEnumerable<DeskEntity> Desks { get; set; }
    }
}
