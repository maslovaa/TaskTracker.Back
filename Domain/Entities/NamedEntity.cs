namespace Domain.Entities;

/// <summary>
/// Базовая сущность с идентификатором и именем.
/// </summary>
/// <typeparam name="T">Тип идентификатора.</typeparam>
public abstract class NamedEntity : IdEntity
{
    /// <summary>
    /// Имя сущности.
    /// </summary>
    public string Name { get; set; }
}