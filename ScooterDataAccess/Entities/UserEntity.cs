using System.ComponentModel.DataAnnotations.Schema;

namespace ScooterDataAccess.Entities;
using Microsoft.AspNetCore.Identity;

[Table("User")]
public class UserEntity : IdentityUser<int>, IBaseEntity
{
    public Guid ExternalId { get; set; }
    public DateTime ModificationTime { get; set; }
    public DateTime CreationTime { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public string? Patronymic { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Mail { get; set; }
    public string PasswordHash { get; set; }
    public string Login { get; set; }
    public ICollection<TripEntity> Trips { get; set; }
    public ICollection<TechnicalInspectionEntity> TechnicalInspections { get; set; }
}

public class UserRole : IdentityUserRole<int>
{
}