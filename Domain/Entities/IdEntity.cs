using Domain.Abstractions;

namespace Domain.Entities;

/// <summary>
/// Базовая сущность с идентификатором.
/// </summary>
public abstract class IdEntity : IEntity<Guid>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
}