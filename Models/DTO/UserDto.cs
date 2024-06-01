namespace Models.Dto;

public class UserDto
{
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public RoleDto Role { get; set; }
}