namespace ScooterService.Controllers.Users.Entities;

public class RegisterUserRequest
{
    public string Surname { get; set; }
    public string Name { get; set; }
    public string? Patronymic { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Mail { get; set; }
    public string PasswordHash { get; set; }
    public string Login { get; set; }
    public int RoleId { get; set; }
}