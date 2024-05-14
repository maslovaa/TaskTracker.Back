namespace Models.Dto;

public class CreatingUserDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}