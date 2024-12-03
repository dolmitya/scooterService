using AutoMapper;
using BL.Users.Entitiy;
using BL.Users.Exceptions;
using ScooterDataAccess.Entities;
using ScooterDataAccess.Repository;

namespace BL.Users.Provider;

public class UsersProvider : IUsersProvider
{
    private readonly IRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public UsersProvider(IRepository<UserEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public IEnumerable<UserModel> GetUsers(FilterUserModel filter = null)
    {
        string? loginPart = filter?.LoginPart;
        string? surnamePart = filter?.SurnamePart;
        string? mailPart = filter?.MailPart;
        DateTime? dateOfBirthPart = filter?.DateOfBirthPart;
        DateTime? creationTime = filter?.CreationTime;
        DateTime? modificationTime = filter?.ModificationTime;
        int? role = filter?.Role;
    
    

        var users = _userRepository.GetAll(u =>
            (loginPart == null || u.Login == loginPart) &&
            (surnamePart == null || u.Surname.Contains(surnamePart)) &&
            (dateOfBirthPart == null || u.DateOfBirth == dateOfBirthPart) &&
            (mailPart == null || u.Mail.Contains(mailPart)) &&
            (creationTime == null || u.CreationTime == creationTime) &&
            (modificationTime == null || u.ModificationTime == modificationTime) /*&&
            (role == null || u.Role.Id == role)*/
        );
        return _mapper.Map<IEnumerable<UserModel>>(users);
    }

    public UserModel GerUserInfo(int id)
    {
        var entity = _userRepository.GetById(id);
        if (entity == null)
        {
            throw new UserNotFoundException("User not found");
        }

        return _mapper.Map<UserModel>(entity);
    }
}