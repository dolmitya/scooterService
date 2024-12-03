using AutoMapper;
using BL.Users.Entitiy;
using BL.Users.Manager;
using BL.Users.Provider;
using Microsoft.AspNetCore.Mvc;
using ScooterDataAccess.Controllets.Entities.UserEntities;
using ScooterService.Controllers.Users.Entities;
using ScooterService.Validator.User;
using ILogger = Serilog.ILogger;

namespace ScooterService.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersManager _usersManager;
    private readonly IUsersProvider _usersProvider;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public UsersController(IUsersManager usersManager, IUsersProvider usersProvider,
        IMapper mapper, ILogger logger)
    {
        _usersManager = usersManager;
        _usersProvider = usersProvider;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult RegisterUser([FromBody] RegisterUserRequest request)
    {
        var validationResult = new RegisterUserRequestValidator().Validate(request);
        if (validationResult.IsValid)
        {
            var createUserModel = _mapper.Map<CreateUserModel>(request);
            var userModel = _usersManager.CreateUser(createUserModel);
            return Ok(new UsersListResponse()
            {
                Users = [userModel]
            });
        }

        _logger.Error(validationResult.ToString());
        return BadRequest(validationResult.ToString());
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _usersProvider.GetUsers();
        return Ok(new UsersListResponse()
        {
            Users = users.ToList()
        });
    }

    [HttpGet]
    [Route("filter")]
    public IActionResult GetFilteredUsers([FromQuery] UserFilter filter)
    {
        var userFilterModel = _mapper.Map<FilterUserModel>(filter);
        var users = _usersProvider.GetUsers(userFilterModel);
        return Ok(new UsersListResponse()
        {
            Users = users.ToList()
        });
    }
}