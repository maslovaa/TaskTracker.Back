using Domain.Abstractions;

namespace Domain.Entities;

/// <summary>
/// Базовая сущность с идентификатором.
/// </summary>
/// <typeparam name="T">Тип идентификатора.</typeparam>
public abstract class IdEntity<T> : IEntity<T>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public T Id { get; set; }
}