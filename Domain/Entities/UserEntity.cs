using Domain.Abstractions;

namespace Domain.Entities;

/// <summary>
/// Модель пользователя.
/// </summary>
public class UserEntity : NamedEntity
{
    /// <summary>
    /// Фамилия.
    /// </summary>
    public string Surname { get; set; }
    
    /// <summary>
    /// Отчество.
    /// </summary>
    public string? Patronymic { get; set; }
    
    /// <summary>
    /// Имя пользователя в системе.
    /// </summary>
    public string UserName { get; set; }
    
    /// <summary>
    ///  Электронная почта.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Признак активности.
    /// </summary>
    public bool IsActive { get; set; }
    /// <summary>
    /// Проекты.
    /// </summary>
    public IEnumerable<ProjectEntity> Projects { get; set; }
    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public RoleEntity Role { get; set; }
    /// <summary>
    /// Задачи пользователя.
    /// </summary>
    public IEnumerable<TaskEntity> Tasks { get; set; }
}