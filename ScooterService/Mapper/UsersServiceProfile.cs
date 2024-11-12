using AutoMapper;
using BL.Users.Entitiy;
using ScooterDataAccess.Controllets.Entities.UserEntities;
using ScooterService.Controllers.Users.Entities;

namespace ScooterService.Mapper;

public class UsersServiceProfile : Profile
{
    public UsersServiceProfile()
    {
        CreateMap<UserFilter, FilterUserModel>();
        CreateMap<RegisterUserRequest, CreateUserModel>();
    }
}