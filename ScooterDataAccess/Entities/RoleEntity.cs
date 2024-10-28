namespace ScooterDataAccess.Entities;

public class RoleEntity : BaseEntity
{
    public string? RoleName { get; set; }
    public ICollection<UserEntity>? Users { get; set; }
}