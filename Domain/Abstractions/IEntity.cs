namespace Domain.Abstractions;

/// <summary>
/// Интерфейс сущности с идентификатором.
/// </summary>
/// <typeparam name="T">Тип идентификатора.</typeparam>
public interface IEntity<T>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public T Id { get; set; }
}