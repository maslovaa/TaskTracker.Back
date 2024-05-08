namespace Domain.Entities;

/// <summary>
/// Базовая сущность с идентификатором и именем.
/// </summary>
public abstract class NamedEntity : IdEntity
{
    /// <summary>
    /// Имя сущности.
    /// </summary>
    public string Name { get; set; }
}