using BL.Users.Entitiy;

namespace BL.Users.Provider;

public interface IUsersProvider
{
    IEnumerable<UserModel> GetUsers(FilterUserModel filter = null);
    UserModel GerUserInfo(int id);
}