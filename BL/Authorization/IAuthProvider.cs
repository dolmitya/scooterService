using BL.Authorization.Entities;
using BL.Users.Entitiy;

namespace BL.Authorization;

public interface IAuthProvider
{
    Task<UserModel> RegisterUser(string email, string password);
    Task<TokensResponse> AuthorizeUser(string email, string password);
}