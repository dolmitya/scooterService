namespace BL.Users.Entitiy;

public class CreateUserModel
{
    public string Surname { get; set; }
    public string Name { get; set; }
    public string? Patronymic { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Login { get; set; }
    public int RoleId { get; set; }
}