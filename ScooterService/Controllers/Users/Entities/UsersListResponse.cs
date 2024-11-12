using BL.Users.Entitiy;

namespace ScooterService.Controllers.Users.Entities;

public class UsersListResponse
{
    public List<UserModel> Users { get; set; }
}